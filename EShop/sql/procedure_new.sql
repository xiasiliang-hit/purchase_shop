/*
procedure for database 'PurchaseWeb'
based on tables in  following files:
User.sql
Commodity.sql
Message.sql
*/

create proc isRegisted
@id nvarchar(50),
@pwd nvarchar(50),
@return bit output
as
declare @@temp_pwd nvarchar(50)
begin
	select @@temp_pwd=pwd
	from RegistedUser
	where id = @id

	if @@temp_pwd = @pwd
	begin
		set @return = 1
		select * from RegistedUser
		where id = @id
	end
	else
	begin
		set @return = 0
	end
end
go

--�����û�
create proc insertUser
@id nvarchar(50),
@pwd nvarchar(50),
@nickname nvarchar(50),
@email nvarchar(50),
@phone nvarchar(50),
@zonestyleid nvarchar(50),
@portraitPath nvarchar(500),
@city nvarchar(50),
@school nvarchar(50),
@address nvarchar(50)
--@type int default 1==nopay

--@pay_amount, float default=0
--@payendtime datetime default=null
as
begin
	insert into RegistedUser
	values(@id, @pwd, @nickname, @email, @phone, @zonestyleid, @portraitPath, @city, @school, @address, 1, 0, null) --$
end
go

--���Ҹ����̼ҵķ��������ؼ��ֵ���Ʒ
--���������𣬲���Kind����"ALL"
create proc searchPaymentCommodity
@keyword nvarchar(50),
@kind int
as
begin
--	select id as matchUserID
--	from RegistedUser
--	where type = 2
--
--	select commodityid as matchCommodityID
--	from TagAndCommodity
--	where tagid like ('%' + @keyword + '%')
select * from Commodity
	if @kind = 0
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in (select commodityid as matchCommodityID
															from TagAndCommodity
															where tagid like ('%' + @keyword + '%'))
				)
			and userfrom in (select id as matchUserID
							from RegistedUser
							where type = 2)
--			and kind = @kind
		end
	else
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in (select commodityid as matchCommodityID
															from TagAndCommodity
															where tagid like ('%' + @keyword + '%'))
				)
			and userfrom in (select id as matchUserID
							from RegistedUser
							where type = 2)
			and kind = @kind
		end
end
go

--���ҷǸ����̼ҵķ��������ؼ��ֵ���Ʒ
--���������𣬲���Kind����"ALL"
create proc searchNoPaymentCommodity
@keyword nvarchar(50),
@kind int
as
begin
--	select id as PayID from RegistedUser
--	where type = 1
--
--	select commodityid as matchCommodityID
--	from TagAndCommodity
--	where tagid like ('%' + @keyword + '%')
select * from Commodity
	if @kind = 0
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in (select commodityid as matchCommodityID
															from TagAndCommodity
															where tagid like ('%' + @keyword + '%'))
				)
			and userfrom in (select id as matchUserID
							from RegistedUser
							where type = 1)
--			and kind = @kind
		end
	else
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in (select commodityid as matchCommodityID
															from TagAndCommodity
															where tagid like ('%' + @keyword + '%'))
				)
			and userfrom in (select id as matchUserID
							from RegistedUser
							where type = 1)
			and kind = @kind
		end
end
go

--����û�������Ϣ
create proc getUserByUserName
@user_id nvarchar(50)
as
begin
	select * from RegistedUser
	where id = @user_id
end
go

--����û����з�������Ʒ
create proc getCommodityByUser
@user_id nvarchar(50)

as
begin
	select * from Commodity
	where userfrom = @user_id
end
go

--������Ʒ
create proc insertCommodity
@id uniqueidentifier,
@userfrom nvarchar(50),
@name nvarchar(50),
@discription nvarchar(500),
@starttimme datetime,
@endtime datetime,

@price float,
@picturepath nvarchar(500),
@kind int,
@popularity int
as
begin
	insert into Commodity
	values (@id, @userfrom, @name, @discription, @starttimme, @endtime,@price,@picturepath, @kind, @popularity)
end
go

--��������Ʒ��Ϣ
create proc insertDemandCommodity
@id uniqueidentifier,
@userfrom varchar(50),
@name varchar(50),
@discription varchar(500),
@starttime datetime,
@endtime datetime,

@price float,
@kind int
--@popularity int
as
begin
	insert into DemandCommodity
	values (@id,@userfrom,@name,@discription,@starttime,@endtime,@price,@kind,0)
end
go

--��÷��Ϲؼ��ֵ�����Ϣ
create proc searchDemandInfo
@keyword nvarchar(50),
@kind int
as
begin
select * from DemandCommodity
	if @kind = 0
		begin
		select * from DemandCommodity
		where (name like ('%' + @keyword + '%'))
		end
	else
		begin
		select * from DemandCommodity
		where (name like ('%' + @keyword + '%')
			and kind = @kind)
		end
end
go

--�õ�δ��˽����Ϣ
create proc getUnreadPrivateMessageByUser
@user_id nvarchar(50)
as

begin
	select * from Message
	where userto = @user_id
		and type = 1
		and state = 1
end
go

--�õ�δ����������
create proc getUnreadPublicMessageByUser
@user_id nvarchar(50)
as
begin
	select * from Message
	where userto = @user_id
		and type = 2
		and state = 1
end
go

--�õ�����˽����Ϣ
create proc getAllPrivateMessageByUser
@user_id nvarchar(50)
as
begin
	select * from Message
	where userto = @user_id
		and type = 1
end
go

--�õ����й�������
create proc getAllPublicMessageByUser
@user_id nvarchar(50)
as
begin
	select * from Message
	where userto = @user_id
		and type = 2
end
go

--��ø�������
create proc getCommentByUser
@user_id nvarchar(50)
as
begin
	select * from Comment
	where userto = @user_id
end
go

--���վ��������
create proc getNumPrivateMessageByUser
@user_id nvarchar(50)
as
declare @@num int
begin
	select @@num = count(*) from Message
	where userfrom = @user_id
		and type = 1
	return @@num
end
go

--��ø��˹�����������
create proc getNumPublicMessageByUserName
@user_id nvarchar(50)
as
declare @@num int
begin
	select @@num = count(*) from Message
	where userfrom = @user_id
		and type = 2
	return @@num
end
go

--��ø�����������
create proc getNumCommentByUser
@user_id nvarchar(50)
as
declare @@num int
begin
	select @@num = count(*) from Message
	where userfrom = @user_id and
		type = 1
	return @@num
end
go

--������µĶ���
create proc getOrderByUserfrom
@user_id nvarchar(50)

as
begin
	select * from CommodityOrder
	where userfrom = @user_id
end
go

--��������յ��Ķ���
create proc getOrderByUserto
@user_id nvarchar(50)
as
begin
	select * from CommodityOrder
	where userto = @user_id
end
go

create proc getCommodityByOreder
@order_id int
as
begin
	select * from Commodity
	where id in
		(select commodityid from OrderAndCommodity
			where orderid = @order_id)
end
go

--����δ����Ϣ״̬
create proc updateMessageStateByID
@id uniqueidentifier
as
begin
	update Message
	set state = 0
	where id = @id and state = 1
end
go

--���¶����Ѷ�״̬
create proc updateOrderStateByID
@id int
as
begin
	update CommodityOrder
	set state = 0
	where id = @id
end
go

--ɾ����Ϣ
--include private message and public message
create proc deleteMessageByID
@id uniqueidentifier
as
begin
	delete from Meassage
	where id = @id
end
go

--������Ϣ
create proc insertMessage
@id uniqueidentifier,
@userfrom nvarchar(50),
@userto nvarchar(50),

@content nvarchar(500),
--@state int, default = 1 = unread
@type int
as
begin
	insert into Message
	values (@id, @userfrom, @userto, @content, 1, @type)
end
go

--������Ʒ�ȶ�

--update commodity and its tags
create proc updatePopularityCommodity
@id uniqueidentifier

as
begin
	update Commodity
	set popularity = popularity + 1
	where id = @id

	update Tag
	set popularity = popularity + 1
	where id in
		(select tagid from TagAndCommodity
		where commodityid = @id)
end
go

--����tag�ȶ�
/*
create proc updatePopularityTag
@id nvarchar(50)
as
begin
	if @id in (select id from Tag)
	begin
		update Tag
		set popularity = popularity + 1
		where id = @id
	end
	else
	begin
		insert into Tag
		values (@id, 0)
	end
end
go
*/

--��������
create proc insertOrder
--@id uniqueidentifier,
@userfrom nvarchar(50),
@userto nvarchar(50)
--@state=1=unread
as
declare @@id int
begin
	insert into CommodityOrder
	values (@userfrom,@userto,1)

	select @@id = id from inserted
	return @@id
end
go

--����������Ʒ
create proc insertOrderCommodity
@order_id int,
@commodity_id uniqueidentifier
as
begin
	insert into OrderAndCommodity
	values (@order_id,@commodity_id)
end
go

--�����ղ�
create proc insertCollect
@user_id nvarchar(50),
@commodity_id uniqueidentifier

as
begin
	insert into Collect
	values (@user_id, @commodity_id)
end
go

--ɾ���ղ�
create proc deleteCollect
@user_id nvarchar(50),
@commodity_id uniqueidentifier
as
begin
	delete from Collect
	where userid = @user_id and commodityid = @commodity_id
end
go

--����û������ղ�
create proc getCollectByUser
@user_id nvarchar(50)
as
begin
	select * from Commodity
	where id in
	(select commodityid from Collect
	where userid = @user_id)
end
go

--������ע
create proc insertAttention
@user_id_pay nvarchar(50),
@user_id_gain nvarchar(50)
as
begin
	insert into Attention
	values (@user_id_pay, @user_id_gain)
end
go

--ɾ����ע
create proc deleteUserAttention
@id_pay nvarchar(50),
@id_gain nvarchar(50)
as
begin
	delete from Attention
	where idpay = @id_pay and idgain = @id_gain
end
go

--��ø������й�ע
create proc getAttentionByUser
@id_pay nvarchar(50)
as
begin
	select * from RegistedUser
	where id in
		(select idgain from Attention
		where idpay = @id_pay)
end
go

--����û���ʽ�ļ�����·��
create proc getStyleByUser
@user_id nvarchar(50),
@return nvarchar(500) output
as
declare @@fileurl nvarchar(500)
begin
	--select @@fileurl = fileurl
	select @return = fileurl
	from ZoneStyle
	where id =
		(select zonestyleid from RegistedUser
		where id = @user_id)
--return @@fileurl
end
go

--����û�ͷ���ļ�����·��
create proc getPortraitPathByUser
@id nvarchar(50),
@return nvarchar(500) output
as
declare @@path nvarchar(500)
begin
	select @return = portraitPath
	from RegistedUser
	where id = @id
	--return @@path
end
go

--����Tag

---insert or increase
create proc insertTag
@tag_id nvarchar(50),
@commodity_id uniqueidentifier
as
begin
	insert into TagAndCommodity
	values (@tag_id, @commodity_id)

	if @id in (select id from Tag)
	begin
		update Tag
		set popularity = popularity + 1
		where id = @id
	end
	else
	begin
		insert into Tag
		values (@tag_id, 0)
	end
end
go


--�õ���ƷTag
create proc getTagByCommodity
@commodity_id uniqueidentifier
as
begin
	select * from Tag
	where id in
		(select tagid from TagAndCommodity
		where commodityid = @commodity_id)
end
go

---�����û�
create proc updateRegistedUser
@id nvarchar(50),
@pwd nvarchar(50),
@nickname nvarchar(50),
@email nvarchar(50),
@phone nvarchar(50),
@zonestyleid nvarchar(50),
@portraitPath nvarchar(500),
@city nvarchar(50),
@school nvarchar(50),
@address nvarchar(50),

@type int,
@payamount float,
@payendtime datetime
as
begin
	delete from RegistedUser
	where id = @id

	insert into RegistedUser
	values(@id,
		@pwd,
		@nickname,
		@email,
		@phone,
		@zonestyleid,
		@portraitPath,
		@city,
		@school,
		@address,
		@type,
		@payamount,
		@payendtime)
end
go

--������и����û���Ϣ
create proc getAllPayUser
as
begin
	select * from RegistedUser
	where type = 2
end
go

--ɾ������
create proc deleteComment
@id uniqueidentifier
as
begin
	delete from Comment
	where id = @id
end
go

--�õ�����Ͷ��
create proc getAllComplaint
as
begin
	select * from Complaint
end
go



/*
create proc searchPaymentCommodity
@keyword nvarchar(50),
@kind nvarchar(50)
as
begin
	select id as matchUserID
	from RegistedUser
	where type = 2

	select commodityid as matchCommodityID
	from TagAndCommodity
	where tagid like ('%' + @keyword + '%')

	if @kind = 'ALL'
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in matchCommodityID)
			and userfrom in matchUserID
		end
	else
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in matchCommodityID)
			and userfrom in PayID
			and kind = @kind
		end
end

create proc searchNoPaymentCommodity
@keyword nvarchar(50),
@kind nvarchar(50)
as
begin
	select id as PayID from RegistedUser
	where type = 1

	select commodityid as matchCommodityID
	from TagAndCommodity
	where tagid like ('%' + @keyword + '%')

	if @kind = 'ALL'
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in matchCommodityID)
			and userfrom in matchUserID
		end
	else
		begin
		select * from Commodity
		where (name like ('%' + @keyword + '%') or id in matchCommodityID)
			and userfrom in PayID
			and kind = @kind
		end
end


*/



--matchs when keyword is in nickname phone
create proc searchRegistedUser
@keyword nvarchar(50)
as
declare @@matchword nvarchar(50)
begin
set @@matchword = '%' + @keyword + '%'

		select * from RegistedUser
		where (nickname like @@matchword
			or phone like @@matchword)
end
go

--�õ���Ʒ
create proc getCommodityByID
@commodity_id uniqueidentifier
as
begin
	select * from Commodity
	where id = @commodity_id
end
go

--��������
create proc insertComment
	@id uniqueidentifier,
	@userfrom nvarchar(50),
	@userto nvarchar(50),

	@content nvarchar(500),

	--@state int, 1==unread
	@commodityid uniqueidentifier
as
begin
	insert into Comment
	values (@id, @userfrom, @userto, @content, 1, @commodityid)
end
go

--����Ͷ��
create proc insertComplaint
	@id uniqueidentifier,
	@userfrom nvarchar(50),
	@userto nvarchar(50),

	@content nvarchar(500),

	--@state int, 1==unread
	@commodityid uniqueidentifier
as
begin
	insert into Complaint
	values (@id, @userfrom, @userto,  @content, 1, @commodityid)
end
go

--����δ����������
create proc updateCommentStateByID
@id uniqueidentifier
as
begin
	update Commment
	set state = 0
	where id = @id
end
go

--����Ͷ��δ��״̬
create proc updateComplaintStateByID
@id uniqueidentifier
as
begin
	update Complaint
	set state = 0
	where id = @id
end
go



----
------


create proc getAllTag
as
begin
	select * from Tag
	order by popularity	desc
end


create proc getAllDemandCommodity
as
begin
	select * from DemandCommodity
end