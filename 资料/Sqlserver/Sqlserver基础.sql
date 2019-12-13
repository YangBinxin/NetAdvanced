/*��������

 * bigint -2^63 (-9,223,372,036,854,775,808) �� 2^63-1 (9,223,372,036,854,775,807)
 * int  -2^31 (-2,147,483,648) �� 2^31-1 (2,147,483,647)
 * smallint  -2^15 (-32,768) �� 2^15-1 (32,767)
 * tinyint  0 �� 255

 * decimal �� numeric  - 10^38 +1 �� 10^38 - 1   [p,s] p���� sС��λ��
 * bit ����ȡֵΪ 1��0 �� NULL �� integer ��������

 * money -922,337,203,685,477.5808 �� 922,337,203,685,477.5807
 * smallmoney -214,748.3648 �� 214,748.3647

 * float -1.79E + 308 �� -2.23E - 308��0 �Լ� 2.23E - 308 �� 1.79E + 308   nΪ1-24 4���ֽ� 24-53 8���ֽ�
 * real -3.40E + 38 �� -1.18E - 38��0 �Լ� 1.18E - 38 �� 3.40E + 38  4���ֽ�

 * date  yyyy-MM-dd  0001-01-01 �� 9999-12-31
 * datetimeoffset YYYY-MM-DD hh:mm:ss[.nnnnnnn] [{+|-}hh:mm] ��Ԫ 1 �� 1 �� 1 �յ���Ԫ 9999 �� 12 �� 31 ��
 * datetime2 YYYY-MM-DD hh:mm:ss[.fractional seconds]  ��Ԫ 1 �� 1 �� 1 �յ���Ԫ 9999 �� 12 �� 31 ��   ������7λ
 * smalldatetime 1900-01-01 �� 2079-06-06
 * datetime 1753 �� 1 �� 1 �յ� 9999 �� 12 �� 31 ��  ������3λ
 * time Ϊ hh:mm:ss[.nnnnnnn]  00:00:00.0000000 �� 23:59:59.9999999

 * char  ��С�̶�  1 �� 8,000
 * varchar ��С�ɱ�  2^31-1  
 * text �� Unicode ����  2^31-1 

 * nchar ��С�̶�  1 �� 4,000 
 * nvarchar ��С�ɱ�  2^30-1  ��˫�ֽ�Ϊ��λ
 * ntext ���ȿɱ�� Unicode ����   2^30 - 1  �洢��С���������ַ������ȵ�����

 * binary �̶����ȶ���������  1-8000
 * varbinary �ɱ䳤�ȶ��������� 1-8000  max 2^31-1 
 * image ���ȿɱ�Ķ��������ݣ�0 �� 2^31-1

 * cursor ���Ǳ�����洢���� OUTPUT ������һ���������ͣ���Щ�����������α������
 * rowversion ֻ�ǵ��������֣����������ڻ�ʱ��  �������ݿ����Զ����ɵ�Ψһ���������ֵ���������
 * hierarchyid ���ȿɱ��ϵͳ��������,��ʾ��νṹ�е�λ��
 * uniqueidentifier 16 �ֽ� GUID
 * sql_variant �洢 SQL Server ֧�ֵĸ����������͵�ֵ
 * xml �洢 XML ���ݵ��������͡� �������л��� xml ���͵ı����д洢 xml ʵ��
 * geometry �ռ伸������
 * geography �ռ��������
 * table ��Ҫ������ʱ�洢һ����Ϊ��ֵ������������ص���
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
print '��������п���Ϊ��'+@s

select CONCAT('abc',SPACE(5),'def'),len(CONCAT('abc',SPACE(5),'def'))

select REPLICATE('abc',3)

select STUFF('�����人',2,2,'��')

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

--��ӷǿ�Լ��
alter table Exam alter column StuId int not null 
alter table Exam alter column StuId int null 

--���ΨһԼ��
alter table StuInfo add constraint UQ_StuName unique(StuName)
alter table StuInfo drop constraint UQ_StuName
--Ψһ�������������в�Ϊ��ʱ���Ž�����֤Ψһ
CREATE UNIQUE NONCLUSTERED INDEX xx on 
����ProductDemo(<������>)����--ָ��������
����where <������>!=null)����--��������

-- ���Ĭ��Լ��
alter table StuInfo add constraint DF_StuSex Default('��') for StuSex
alter table StuInfo drop constraint DF_StuSex

-- ��Ӽ��Լ��
alter table StuInfo add constraint CK_StuAge check(StuAge>18 and StuAge<30)
alter table StuInfo drop constraint CK_StuAge

--�������Լ��
alter table StuInfo add constraint PK_U_ID primary key (U_ID)
alter table StuInfo drop constraint PK_U_ID

-- ������Լ��
alter table Exam add constraint Fk_StuId foreign key (StuId) references StuInfo(StuId)
alter table Exam drop constraint Fk_StuId


--��Ӿۼ�����
create NCLUSTERED index NC_StuId on StuInfo(StuId)
drop index NC_StuId on StuInfo

--��ӷǾۼ�����
create index NO_Index1 on StuInfo(StuName asc,StuSex,StuAge desc)
drop index NO_Index1 on StuInfo

--���Ψһ�ۼ�����
create unique NCLUSTERED index NC_StuId on StuInfo(StuId)
drop index NC_StuId on StuInfo

--���Ψһ�Ǿۼ�����
create unique index NO_Index1 on StuInfo(StuName asc,StuSex,StuAge desc)
drop index NO_Index1 on StuInfo


declare @count int = 0
while(1=1)
begin
if(@count > 200000)
begin
break
end
insert into StuInfo(StuName,StuAge) values(CONCAT('����_',SUBSTRING(cast(RAND() as varchar),3,5)),20)
set @count = @count + 1
end
select * from StuInfo

select Str(RAND(),10,5)
select cast(RAND() as varchar),SUBSTRING(cast(RAND() as varchar),3,5)
select SUBSTRING('0.80487',3,5)
select CONCAT('����_',SUBSTRING(cast(RAND() as varchar),3,5))

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

--����Insert_Exam�洢����
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


--������ô洢����Insert_Exam
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
	print '�������'
	rollback
end
else
begin
	print '����ɹ�'
	commit tran
end

select * from Exam

