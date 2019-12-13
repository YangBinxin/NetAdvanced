/*数据类型

 * bigint -2^63 (-9,223,372,036,854,775,808) 到 2^63-1 (9,223,372,036,854,775,807)
 * int  -2^31 (-2,147,483,648) 到 2^31-1 (2,147,483,647)
 * smallint  -2^15 (-32,768) 到 2^15-1 (32,767)
 * tinyint  0 到 255

 * decimal 和 numeric  - 10^38 +1 到 10^38 - 1   [p,s] p精度 s小数位数
 * bit 可以取值为 1、0 或 NULL 的 integer 数据类型

 * money -922,337,203,685,477.5808 到 922,337,203,685,477.5807
 * smallmoney -214,748.3648 到 214,748.3647

 * float -1.79E + 308 至 -2.23E - 308、0 以及 2.23E - 308 至 1.79E + 308   n为1-24 4个字节 24-53 8个字节
 * real -3.40E + 38 至 -1.18E - 38、0 以及 1.18E - 38 至 3.40E + 38  4个字节

 * date  yyyy-MM-dd  0001-01-01 到 9999-12-31
 * datetimeoffset YYYY-MM-DD hh:mm:ss[.nnnnnnn] [{+|-}hh:mm] 公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日
 * datetime2 YYYY-MM-DD hh:mm:ss[.fractional seconds]  公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日   毫秒数7位
 * smalldatetime 1900-01-01 到 2079-06-06
 * datetime 1753 年 1 月 1 日到 9999 年 12 月 31 日  毫秒数3位
 * time 为 hh:mm:ss[.nnnnnnn]  00:00:00.0000000 到 23:59:59.9999999

 * char  大小固定  1 到 8,000
 * varchar 大小可变  2^31-1  
 * text 非 Unicode 数据  2^31-1 

 * nchar 大小固定  1 到 4,000 
 * nvarchar 大小可变  2^30-1  以双字节为单位
 * ntext 长度可变的 Unicode 数据   2^30 - 1  存储大小是所输入字符串长度的两倍

 * binary 固定长度二进制数据  1-8000
 * varbinary 可变长度二进制数据 1-8000  max 2^31-1 
 * image 长度可变的二进制数据，0 到 2^31-1

 * cursor 这是变量或存储过程 OUTPUT 参数的一种数据类型，这些参数包含对游标的引用
 * rowversion 只是递增的数字，不保留日期或时间  公开数据库中自动生成的唯一二进制数字的数据类型
 * hierarchyid 长度可变的系统数据类型,表示层次结构中的位置
 * uniqueidentifier 16 字节 GUID
 * sql_variant 存储 SQL Server 支持的各种数据类型的值
 * xml 存储 XML 数据的数据类型。 可在列中或者 xml 类型的变量中存储 xml 实例
 * geometry 空间几何类型
 * geography 空间地理类型
 * table 主要用于临时存储一组作为表值函数结果集返回的行
 */

select ISNULL(null,'tihuan')

declare @rand numeric(20,8)
select @rand=RAND()
print @rand
print STR(@rand,10,8)
declare @s varchar(16) 
set @s=SUBSTRING(STR(@rand,10,8),3,8)
print @s
set @s='62253800'+@s
print '你的新银行卡号为：'+@s

select CONCAT('abc',SPACE(5),'def'),len(CONCAT('abc',SPACE(5),'def'))

select REPLICATE('abc',3)

select STUFF('湖南武汉',2,2,'北')

select CHAR(97),ASCII('a')

if object_ID('StuInfo')is not null
drop table StuInfo
go
create table StuInfo
(
      StuId int identity(1,1) primary key,
      StuName varchar(10) not null,
      StuSex varchar(2) not null,
      StuAge varchar(3) not null
)

if OBJECT_ID('Exam') is not null
drop table Exam
go
create table Exam
(
	StuId int,
	writeExam decimal(5,2)
)

--添加非空约束
alter table Exam alter column StuId int not null 
alter table Exam alter column StuId int null 

--添加唯一约束
alter table StuInfo add constraint UQ_StuName unique(StuName)
alter table StuInfo drop constraint UQ_StuName
--唯一索引，当索引列不为空时，才进行验证唯一
CREATE UNIQUE NONCLUSTERED INDEX xx on 
　　ProductDemo(<索引列>)　　--指定索引列
　　where <索引列>!=null)　　--过滤条件

-- 添加默认约束
alter table StuInfo add constraint DF_StuSex Default('男') for StuSex
alter table StuInfo drop constraint DF_StuSex

-- 添加检查约束
alter table StuInfo add constraint CK_StuAge check(StuAge>18 and StuAge<30)
alter table StuInfo drop constraint CK_StuAge

--添加主键约束
alter table StuInfo add constraint PK_U_ID primary key (U_ID)
alter table StuInfo drop constraint PK_U_ID

-- 添加外检约束
alter table Exam add constraint Fk_StuId foreign key (StuId) references StuInfo(StuId)
alter table Exam drop constraint Fk_StuId


--添加聚集索引
create NCLUSTERED index NC_StuId on StuInfo(StuId)
drop index NC_StuId on StuInfo

--添加非聚集索引
create index NO_Index1 on StuInfo(StuName asc,StuSex,StuAge desc)
drop index NO_Index1 on StuInfo

--添加唯一聚集索引
create unique NCLUSTERED index NC_StuId on StuInfo(StuId)
drop index NC_StuId on StuInfo

--添加唯一非聚集索引
create unique index NO_Index1 on StuInfo(StuName asc,StuSex,StuAge desc)
drop index NO_Index1 on StuInfo


declare @count int = 0
while(1=1)
begin
if(@count > 200000)
begin
break
end
insert into StuInfo(StuName,StuAge) values(CONCAT('测试_',SUBSTRING(cast(RAND() as varchar),3,5)),20)
set @count = @count + 1
end
select * from StuInfo

select Str(RAND(),10,5)
select cast(RAND() as varchar),SUBSTRING(cast(RAND() as varchar),3,5)
select SUBSTRING('0.80487',3,5)
select CONCAT('测试_',SUBSTRING(cast(RAND() as varchar),3,5))

if exists(select*from sys.indexes
where name='IX_stuinfo_Name')
drop index StuInfo.IX_stuinfo_Name
go
create nonclustered index IX_stuinfo_Name
on StuInfo(StuName desc)

select*From StuInfo
with(index=IX_Stuinfo_Name)
where CAST(SUBSTRING(StuName,4,5) as int) > 47700

select SUBSTRING(StuName,4,5) from StuInfo with(index=IX_Stuinfo_Name)

--创建Insert_Exam存储过程
if OBJECT_ID('Insert_Exam') is not null
drop proc Insert_Exam
go
create proc Insert_Exam
@stuid int ,
@writeExam decimal(18,2),
@ladExam decimal(18,2),
@n int output
as
insert into Exam values(@stuid,@writeExam,@ladExam)
select @n = @n+ @@ROWCOUNT
go


--事务调用存储过程Insert_Exam
begin tran
declare @err int = 0,
@stuid int,
@writeExam decimal(18,2) = CAST(SUBSTRING(cast(RAND() as varchar),3,2) as decimal),
@ladExam decimal(18,2) = CAST(SUBSTRING(cast(RAND() as varchar),3,2) as decimal),
@rtnCount int = 0,
@resultRow int = 0,
@minId int,@maxId int,@sql1 nvarchar(500)

set @sql1 = 'select @minId=min(StuId),@maxId=max(StuId) from StuInfo '
exec sp_executesql @sql1,N'@minId int out,@maxId int out',
@minId out,@maxId out
select @minId,@maxId

while(@minId <= @maxId)
begin
	set @stuid = @minId
	set	@writeExam = CAST(SUBSTRING(cast(RAND() as varchar),3,2) as decimal)
	set	@ladExam = CAST(SUBSTRING(cast(RAND() as varchar),3,2) as decimal)
	exec Insert_Exam @stuid,@writeExam,@ladExam,@rtnCount
	set @resultRow  = @resultRow + @rtnCount
	set @minId = @minId + 1
	set @err=@err+@@ERROR
end
if(@err > 0)
begin
	print '插入错误'
	rollback
end
else
begin
	print '插入成功'
	commit tran
end

select * from Exam

