-- һ�����������ļ���/�ļ�
--���������ļ���
alter database DbBeginStudy add filegroup FileGroup1
alter database DbBeginStudy add filegroup FileGroup2
alter database DbBeginStudy add filegroup FileGroup3
alter database DbBeginStudy add filegroup FileGroup4

--���������ļ�
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

-- ����������������
create partition function f_TestDate(datetime)
as range right for values('2016-01-01','2017-01-01','2018-01-01')

-- ����������������
create partition scheme s_TestDate
as partition f_TestDate to (FileGroup1,FileGroup2,FileGroup3,FileGroup4)

-- �ġ��½�������
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

-- �����з��������뺯������ɾ��
select * from sys.partition_schemes --��ѯ����
select * from sys.partition_range_values --��ѯ������Χ
select * from sys.partition_functions --��ѯ��������

DROP TABLE tradelog  --ɾ��ʹ�÷����ı�
DROP PARTITION SCHEME s_TestDate  --ɾ����������
DROP PARTITION FUNCTION f_TestDate  --ɾ����������

-- �塢���ӷ���
--����������1000W������
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

--�鿴���������״
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

--���ϵͳ�����
select * from sys.destination_data_spaces  --����������ÿ�����ݿռ�Ŀ���Ӧһ�С�
select * from sys.indexes  --���ݿ��е�ÿ�������ͱ��ڱ��и�ռһ�С�
select * from sys.sysfilegroups --���ݿ��е�ÿ���ļ����ڱ���ռһ�С�
select * from sys.sysfiles  --���ݿ��е�ÿ���ļ��ڱ���ռһ�С�

select * from sys.partition_schemes  --ÿ�����ݿռ�ķ���������ʹ�ñ���ռһ������
select * from sys.partition_functions  --ÿ������������ռһ�С�
select * from sys.partition_range_values --����Ϊ R �ķ���������ÿ����Χ�߽�ֵ���ڱ���ռһ�С�
select * from sys.partitions  --���б�����������ٰ���һ������

--�����ӷ��� 2019-01-01 0:00:00
--�����·����ļ���
alter database DbBeginStudy add filegroup FileGroup5
--�����·����ļ�
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


-- �����ϲ�����  �����������ı߽��Ƴ�  �����ϲ��󣬽��ϲ��ֽ��ĺ�һ�����������ƶ���ǰһ���������ļ���
alter partition function f_TestDate()
merge range('2018-01-01 0:00:00')

--����4�������ļ���������ݺ�4���������������ݣ����������������ϲ�֮���Ƴ����Ǹ������ļ��ڴ�û���ͷ�
--�ֶ��������ݿ�����


-- �ߡ����������ƶ�����ͨ��  1.��ͨ�����Ͷ�Ӧ�ķ�����ͬһ���ļ�����  2.��ͨ��ͷ�����ṹ��ͬ�������ֶΡ��������͡����ݳ��ȡ�������
create table tradelog_partition1  --���һ���� ͬΪһ���ļ���
(
	ID int,
	productID int,
	tradedate datetime
) on FileGroup1
--�����ۼ�����
CREATE CLUSTERED INDEX [CLI_tn_TestDate_1] ON [dbo].[tradelog_partition1] 
(
	[tradedate]
)
alter table tradelog switch partition 1 to tradelog_partition1


-- �ˡ�����ͨ���ƶ���������   1.��ͨ����Ҫ��ӷ���check����Լ��  2.ָ����������Ϊ��
alter table tradelog_partition1 switch to tradelog partition 1
ALTER TABLE dbo.tradelog_partition1
	ADD CONSTRAINT TradeDate_Switch_CHECK CHECK 
	(TradeDate >= CONVERT(DATE,'2014-01-01') AND TradeDate < CONVERT(DATE,'2016-01-01')
    AND TradeDate IS NOT NULL);


-- �š��Զ������洢����
go
create proc JobAutoPartity as
DECLARE @fileGroupName VARCHAR(200),
    @fileNamePath    VARCHAR(200),
    @fileName   VARCHAR(200),
    @sql        NVARCHAR(1000),
	@rowCount int

SET @fileGroupName='FileGroup'+SUBSTRING(CONVERT(varchar(100), GETDATE(), 112),1,6)
PRINT '�ļ�������'+@fileGroupName

select @rowCount=count(*) from sys.sysfilegroups where groupname = @fileGroupName
if(@rowCount > 0)
begin
	print '�����ļ����Ѵ���'
	return
end
else
begin
	SET @sql='ALTER DATABASE [DbBeginStudy] ADD FILEGROUP '+@fileGroupName
	PRINT '��ӷ��� �ļ���'+@sql
	EXEC(@sql)
	select top 1 @fileNamePath=Replace(filename,name,CONCAT('FileGroup',SUBSTRING(CONVERT(varchar(100), GETDATE(), 112),1,6))) from sys.sysfilegroups a,sys.sysfiles b where a.groupid = b.groupid and groupname like 'FileGroup%'
	print '�����ļ������ַ'+@fileNamePath

	SET @fileName=N'Partity'+SUBSTRING(CONVERT(varchar(100), GETDATE(), 112),1,6)
	PRINT '�����ļ�����'+@fileName

	SET @sql='ALTER DATABASE [DbBeginStudy] ADD FILE (NAME='''+@fileName+''',FILENAME=N'''+@fileNamePath+''') TO FILEGROUP'+'  '+@fileGroupName
	PRINT '1����ӷ����ļ�'+@sql
	EXEC(@sql)
	PRINT '2����ӷ����ļ��ɹ�'

	--�޸ķ�����������һ���µ��ļ������ڴ����һ����������
	SET @sql='ALTER PARTITION SCHEME [s_TestDate] NEXT USED'+'    '+@fileGroupName
	EXEC(@sql)
	--�����ܹ�
	PRINT '3���޸ķ��������ɹ�'
	select @rowCount=count(1) from SYS.PARTITION_RANGE_VALUES where CAST(value as datetime) = CAST(CONCAT(CONVERT(varchar(7), GETDATE(), 23),'-01 00:00:00.000') as datetime)
	print @rowCount
	if(@rowCount > 0)
	begin 
		print '���������Ѵ���'
		return
	end
	else
	begin
		ALTER PARTITION FUNCTION f_TestDate()  --��������
		SPLIT RANGE (CONCAT(CONVERT(varchar(7), GETDATE(), 23),'-01 00:00:00.000'))
		print '�޸ķ�������'
	end
end

-- ʮ��ɾ�������ļ��������ļ���
select * from sys.sysfiles
--��շ����ļ������ݣ�Ȼ��ɾ�������ļ�
DBCC SHRINKFILE ('Partity201908', EMPTYFILE);
ALTER DATABASE [DbBeginStudy] Remove FILE Partity201908

--ɾ�������ļ���
ALTER DATABASE [DbBeginStudy] Remove FILEGROUP FileGroup201908

