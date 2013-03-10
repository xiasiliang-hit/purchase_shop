USE [PurchaseWeb_v1]
GO

DECLARE	@return_value int,

@@id uniqueidentifier
set @@id = newid()
EXEC	@return_value = [dbo].[insertUser]
		@id = @@id,
		@pwd = NULL,
		@nickname = NULL,
		@email = NULL,
		@phone = NULL,
		@zonestyleid = NULL,
		@portraitPath = NULL,
		@city = NULL,
		@school = NULL,
		@address = NULL

SELECT	'Return Value' = @return_value

GO

---------------------
USE [PurchaseWeb_v1]
GO

DECLARE
@return_value int,
@@id uniqueidentifier
set@@id = newid()

EXEC	@return_value = [dbo].[insertCommodity]
		@id = @@id,
		@userfrom = 'xia',
		@name = 'shangping',
		@discription = 'dis',
		@starttimme = '2010-1-1',
		@endtime = '2010-10-10',
		@price = 12,
		@picturepath = '~//web.config',
		@kind = 1,
		@popularity = 1

SELECT	'Return Value' = @return_value

GO

eee1cd05-6d04-4174-8234-b8a9ae65e0a5
eee1cd05-6d04-4174-80d5-b8a9ae65e0b3

----------------------
USE [PurchaseWeb_v1]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[searchNoPaymentCommodity]
		@keyword = "%",
		@kind = "ALL"

SELECT	'Return Value' = @return_value

GO


--------------
USE [PurchaseWeb_v1]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[searchPaymentCommodity]
		@keyword = 'ÄÐµÄ',
		@kind = 2

SELECT	'Return Value' = @return_value

GO

---------
select * from commodity
select * from registeduser
select * from tag
select * from tagandcommodity