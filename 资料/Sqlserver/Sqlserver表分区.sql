-- 一、创建分区文件组/文件
--创建分区文件组
alter database DbBeginStudy add filegroup FileGroup1
alter database DbBeginStudy add filegroup FileGroup2
alter database DbBeginStudy add filegroup FileGroup3
alter database DbBeginStudy add filegroup FileGroup4

--创建分区文件
alter database DbBeginStudy
	add file(name='Partity1'
		,filename='C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Partity1.ndf'
		,size=1mb
		,filegrowth=1mb)
		to filegroup FileGroup1;

alter database DbBeginStudy
	add file(name='Partity2'
		,filename='C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Partity2.ndf'
		,size=1mb
		,filegrowth=1mb)
		to filegroup FileGroup2;

alter database DbBeginStudy
	add file(name='Partity3'
		,filename='C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Partity3.ndf'
		,size=1mb
		,filegrowth=1mb)
		to filegroup FileGroup3;

alter database DbBeginStudy
	add file(name='Partity4'
		,filename='C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Partity4.ndf'
		,size=1mb
		,filegrowth=1mb)
		to filegroup FileGroup4;

-- 二、创建分区函数
create partition function f_TestDate(datetime)
as range right for values('2016-01-01','2017-01-01','2018-01-01')

-- 三、创建分区方案
create partition scheme s_TestDate
as partition f_TestDate to (FileGroup1,FileGroup2,FileGroup3,FileGroup4)

-- 四、新建分区表
create table tradelog
(
	ID int,
	productID int,
	tradedate datetime
) on s_TestDate(tradedate)

CREATE CLUSTERED INDEX [CLI_tn_TestDate] ON [dbo].[tradelog]
(
	[tradedate]
)ON [s_TestDate]([tradedate])

-- 对已有分区方案与函数进行删除
select * from sys.partition_schemes --查询分区
select * from sys.partition_range_values --查询分区范围
select * from sys.partition_functions --查询分区函数

DROP TABLE tradelog  --删除使用分区的表
DROP PARTITION SCHEME s_TestDate  --删除分区方案
DROP PARTITION FUNCTION f_TestDate  --删除分区函数

-- 五、增加分区
--向分区表插入1000W行数据
DECLARE @max AS INT, @rc AS INT;  
SET @max = 10000000;  
SET @rc = 1;  
INSERT INTO tradelog(id,productID,tradedate) VALUES(1,1,'2014-01-01');  
WHILE @rc * 2 <= @max  
BEGIN  
    INSERT INTO dbo.tradelog(id,productID,tradedate) SELECT id + @rc,id + @rc+1,DATEADD(mi,id,tradedate) FROM dbo.tradelog;  
    SET @rc = @rc * 2;  
END  
INSERT INTO dbo.tradelog (id,productID,tradedate)
SELECT id + @rc,id + @rc+1,DATEADD(mi,id,tradedate) FROM dbo.tradelog WHERE id + @rc <= @max;  
go  

--查看分区表的现状
with cte as
(select object_id,OBJECT_NAME(i.object_id) tableName,i.index_id,dds.partition_scheme_id,dds.destination_id as partition_number,fg.groupid,fg.groupname,f.fileid,f.name,f.filename
 from sys.destination_data_spaces dds,sys.indexes i,sys.sysfilegroups fg,sys.sysfiles f
 where dds.partition_scheme_id=i.data_space_id and dds.data_space_id=fg.groupid and fg.groupid=f.groupid ),
cte1 as
(select ps.data_space_id as partition_scheme_id,ps.name partiton_schemes_name,pf.name partition_function_name,pf.function_id
 from sys.partition_schemes ps ,sys.partition_functions pf
 where ps.function_id=pf.function_id)

select cte.tableName,cte.groupname,cte.name,cte.filename
	,cte.partition_number,cte1.partiton_schemes_name,cte1.partition_function_name,p.rows
	,prv.boundary_id,prv.value BoundaryValue
from cte
inner join cte1	on cte.partition_scheme_id=cte1 .partition_scheme_id
left join sys.partition_range_values prv on cte1.function_id=prv.function_id and cte.partition_number=prv.boundary_id
left join sys.partitions  p on cte.object_id=p.object_id and cte.index_id=p.index_id and cte.partition_number=p.partition_number
where cte.object_id=OBJECT_ID('dbo.tradelog','U')

--相关系统标介绍
select * from sys.destination_data_spaces  --分区方案的每个数据空间目标对应一行。
select * from sys.indexes  --数据库中的每个索引和表在表中各占一行。
select * from sys.sysfilegroups --数据库中的每个文件组在表中占一行。
select * from sys.sysfiles  --数据库中的每个文件在表中占一行。

select * from sys.partition_schemes  --每个数据空间的分区方案是使用表中占一行类型
select * from sys.partition_functions  --每个分区函数都占一行。
select * from sys.partition_range_values --类型为 R 的分区函数的每个范围边界值都在表中占一行。
select * from sys.partitions  --所有表和索引都至少包含一个分区

--新增加分区 2019-01-01 0:00:00
--创建新分区文件组
alter database DbBeginStudy add filegroup FileGroup5
--创建新分区文件
alter database DbBeginStudy
	add file(name='Partity5'
		,filename='C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Partity5.ndf'
		,size=1mb
		,filegrowth=1mb)
		to filegroup FileGroup5;
alter partition scheme s_TestDate
next used FileGroup5

alter partition function f_TestDate()
split range('2019-01-01 0:00:00')


-- 六、合并分区  将两个分区的边界移除  分区合并后，将合并分界点的后一个分区数据移动到前一个分区的文件中
alter partition function f_TestDate()
merge range('2018-01-01 0:00:00')

--创建4个分区文件，添加数据后，4个分区都存在数据，把其中两个分区合并之后，移除的那个分区文件内存没有释放
--手动进行数据库收缩


-- 七、分区数据移动到普通表  1.普通表必须和对应的分区在同一个文件组下  2.普通表和分区表结构相同，包括字段、数据类型、数据长度、索引等
create table tradelog_partition1  --与第一分区 同为一个文件组
(
	ID int,
	productID int,
	tradedate datetime
) on FileGroup1
--创建聚集索引
CREATE CLUSTERED INDEX [CLI_tn_TestDate_1] ON [dbo].[tradelog_partition1] 
(
	[tradedate]
)
alter table tradelog switch partition 1 to tradelog_partition1


-- 八、从普通表移动到分区表   1.普通表需要添加分区check条件约束  2.指定分区必须为空
alter table tradelog_partition1 switch to tradelog partition 1
ALTER TABLE dbo.tradelog_partition1
	ADD CONSTRAINT TradeDate_Switch_CHECK CHECK 
	(TradeDate >= CONVERT(DATE,'2014-01-01') AND TradeDate < CONVERT(DATE,'2016-01-01')
    AND TradeDate IS NOT NULL);


-- 九、自动分区存储过程
go
create proc JobAutoPartity as
DECLARE @fileGroupName VARCHAR(200),
    @fileNamePath    VARCHAR(200),
    @fileName   VARCHAR(200),
    @sql        NVARCHAR(1000),
	@rowCount int

SET @fileGroupName='FileGroup'+SUBSTRING(CONVERT(varchar(100), GETDATE(), 112),1,6)
PRINT '文件组名称'+@fileGroupName

select @rowCount=count(*) from sys.sysfilegroups where groupname = @fileGroupName
if(@rowCount > 0)
begin
	print '分区文件组已存在'
	return
end
else
begin
	SET @sql='ALTER DATABASE [DbBeginStudy] ADD FILEGROUP '+@fileGroupName
	PRINT '添加分区 文件组'+@sql
	EXEC(@sql)
	select top 1 @fileNamePath=Replace(filename,name,CONCAT('FileGroup',SUBSTRING(CONVERT(varchar(100), GETDATE(), 112),1,6))) from sys.sysfilegroups a,sys.sysfiles b where a.groupid = b.groupid and groupname like 'FileGroup%'
	print '分区文件保存地址'+@fileNamePath

	SET @fileName=N'Partity'+SUBSTRING(CONVERT(varchar(100), GETDATE(), 112),1,6)
	PRINT '分区文件名称'+@fileName

	SET @sql='ALTER DATABASE [DbBeginStudy] ADD FILE (NAME='''+@fileName+''',FILENAME=N'''+@fileNamePath+''') TO FILEGROUP'+'  '+@fileGroupName
	PRINT '1、添加分区文件'+@sql
	EXEC(@sql)
	PRINT '2、添加分区文件成功'

	--修改分区方案，用一个新的文件组用于存放下一新增的数据
	SET @sql='ALTER PARTITION SCHEME [s_TestDate] NEXT USED'+'    '+@fileGroupName
	EXEC(@sql)
	--分区架构
	PRINT '3、修改分区方案成功'
	select @rowCount=count(1) from SYS.PARTITION_RANGE_VALUES where CAST(value as datetime) = CAST(CONCAT(CONVERT(varchar(7), GETDATE(), 23),'-01 00:00:00.000') as datetime)
	print @rowCount
	if(@rowCount > 0)
	begin 
		print '分区方案已存在'
		return
	end
	else
	begin
		ALTER PARTITION FUNCTION f_TestDate()  --分区函数
		SPLIT RANGE (CONCAT(CONVERT(varchar(7), GETDATE(), 23),'-01 00:00:00.000'))
		print '修改分区函数'
	end
end

-- 十、删除分区文件，分区文件组
select * from sys.sysfiles
--清空分区文件的内容，然后删除分区文件
DBCC SHRINKFILE ('Partity201908', EMPTYFILE);
ALTER DATABASE [DbBeginStudy] Remove FILE Partity201908

--删除分区文件组
ALTER DATABASE [DbBeginStudy] Remove FILEGROUP FileGroup201908

