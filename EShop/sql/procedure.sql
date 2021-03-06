--
create proc is_which_user
@id char(20),
@pwd char(20),
@is_registed bit output
as
declare @@temp_pwd char(20)
begin
	select @@temp_pwd=pwd
	from SysUser
	where id = @id

	if @@temp_pwd = @pwd
	begin
		set @is_registed = 1
		select * from SysUser
		where name = @name
	end
	else
	begin
		set @return = 0
	end
end

--新增用户
create proc insertUser
@id char(20),
@pwd char(20),
@nickname char(20),
@email char(50),
@phone char(20),
@zonestyleid char(50),
@portraitPath char(500),
@city char(20),
@school char(20),
@address char(50)
--@type int default 1==nopay

--@pay_amount, float default=0
--@payendtime datetime default=
as
begin
	insert into RegistedUser
	values(@id, @pwd, @nickname, @email, @phone, @zonestyleid, @portraitPath, @city, @school, @address, 1, 0, null) --$
end

--查找付费商家的符合搜索关键字的商品
--如果所有类别，参数Kind传入"ALL"
create proc searchPaymentCommodity
@keyword char(50),
@kind char(20)
as
begin
	select id as matchUserID
	from RegistedUser
	where type = 2

	select commodityid as matchCommodityID
	from TagAndCommodity
	where tagid like @keyword

	if @kind = 'ALL'
		begin
		select * from Commodity
		where (name like @keyword or id in matchCommodityID)
			and userfrom in matchUserID
		end
	else
		begin
		select * from Commodity
		where (name like @keyword or id in matchCommodityID)
			and userfrom in PayID
			and kind = @kind
		end
end

create proc searchNoPaymentCommodity
@keyword char(50),
@kind char(20)
as
begin
	select id as PayID from RegistedUser
	where type = 1

	select commodityid as matchCommodityID
	from TagAndCommodity
	where tagid like @keyword

	if @kind = 'ALL'
		begin
		select * from Commodity
		where (name like @keyword or id in matchCommodityID)
			and userfrom in matchUserID
		end
	else
		begin
		select * from Commodity
		where (name like @keyword or id in matchCommodityID)
			and userfrom in PayID
			and kind = @kind
		end
end

--获得用户个人信息
create proc getUserByUserName
@user_id char(20)
as
begin
	select * from RegistedUser
	where id = @user_id
end

--获得用户所有发布的商品
create proc getCommodityByUser
@user_id char(20)

as
begin
	select * from Commodity
	where userfrom = @user_id
end

--新增商品
create proc insertCommodity
@id uniqueidentifier,
@userfrom char(20),
@name char(50),
@discription char(500),
@starttimme datetime,
@endtime datetime,

@price float,
@picturepath char(500),
@kind char(20),
@popularity int
as
begin
	insert into Commodity
	values (@id, @userfrom, @name, @discription, @starttimme, @endtime,@price,@picturepath, @kind, @popularity)
end

--新增求购商品信息
create proc insertDemandCommodity
@id uniqueidentifier,
@userfrom char(20),
@name char(50),
@discription char(500),
@starttime datetime,
@endtime datetime,


@kind char(20)
--@popularity int
as
begin
	insert into DemandCommodity
	values (@id,@userfrom,@name,@discription,@starttime,@endtime,@kind,0)
end

--获得符合关键字的求购信息
create proc searchDemandInfo
@keyword char(50),
--@kind char(20)
as
begin
	select * from DemandCommodity
end

--获得个人站内信
create proc getPrivateMessageByUser
@user_id char(20)
as
begin
	select * from Message
	where type = 1
end

--获得个人公共留言
create proc getPublicMessageByUser
@user_id uniqueidentifier
as
begin
	select * from Message
	where userfrom = @user_id and type = 2
end

--获得个人评论
create proc getCommentByUser
@user_id char(20)
as
begin
	select * from Comment
	where userfrom = @user_id
end

--获得站内信数量$
create proc getNumPrivateMessageByUser
@user_id char(20)
as
declare @@num int
begin
	select @@num = count(*) from Message
	where userfrom = @user_id
		and type = 1
	return @@num
end

--获得个人公共留言数量
create proc getNumPublicMessageByUserName
@user_id char(20)
as
declare @@num int
begin
	select @@num = count(*) from Message
	where userfrom = @user_id
		and type = 2
	return @@num
end


--获得个人评论数量
create proc getNumCommentByUser
@user_id char(20)
as
declare @@num int
begin
	select @@num = count(*) from Message
	where userfrom = @user_id and
		type = 1
	return @@num
end



--获得用户下的订单
create proc getOrderByUserfrom
@user_id char(20)

as
begin
	select * from CommodityOrder
	where userfrom = @user_id
end

--获得用户收到的订单
create proc getOrderByUserto
@user_id char(20)
as
begin
	select * from CommodityOrder
	where userto = @user_id
end

create proc getCommodityByOreder
@order_id int
as
begin
	select * from Commodity
	where id in
		(select commodityid from OrderAndCommodity
			where orderid = @order_id)
end

--更新未读消息状态
create proc updateMessageStateByID
@id uniqueidentifier
as
begin
	update Message
	set state = 0
	where id = @id and state = 1
end


--更新订单已读状态
create proc updateOrderStateByID
@id uniqueidentifier

as
begin
	update CommodityOrder
	set state = 0
	where id = @id
end

--删除消息
create proc deleteMessageByID
@id uniqueidentifier

as
begin
	delete from Meassage
	where id = @id
end

--新增消息
create proc insertMessage
@id uniqueidentifier,
@userfrom char(20),
@userto char(20),

@content char(500),
--@state int, -- 0 == read, 1 == unread
@type int

as
begin
	insert into Message
	values (@id, @userfrom, @userto, @content, 1, @type)
end

--更新商品热度
create proc updatePopularityCommodity
@id uniqueidentifier

as
begin
	update Commodity
	set popularity = popularity + 1
	where id = @id
end

--更新tag热度
create proc updatePopularityTag
@id char(20)
as
begin
	select
	update Tag
	set popularity = popularity + 1
	where id = @id
end

--新增订单
create proc insertOrder
--@id uniqueidentifier,
@userfrom char(20),
@userto char(20)
--1=unread
as
begin
	insert into CommodityOrder
	values (@userfrom,@userto,1)
end
go
create proc insertOrderCommodity
@order_id int,
@commodity_id uniqueidentifier
as
begin
	insert into OrderAndCommodity
	values (@order_id,@commodity_id)
end

--新增收藏
create proc insertCollect
@user_id char(20),
@commodity_id uniqueidentifier

as
begin
	insert into Collect
	values (@user_id, @commodity_id)
end

--删除收藏
create proc deleteCollect
@user_id char(20),
@commodity_id uniqueidentifier
as
begin
	delete from Collect
	where userid = @user_id and commodityid = @commodity_id
end

--获得用户所有收藏$
create proc getCollectByUser
@user_id char(20)
as
begin
	select * from Commodity
	where id in
	(select commodityid from Collect
	where userid = @user_id)
end

--新增关注
create proc insertAttention
@user_id_pay char(20),
@user_id_gain char(20)
as
begin
	insert into Attention
	values (@user_id_pay, @user_id_gain)
end

--删除关注
create proc deleteUserAttention
@id_pay char(20),
@id_gain char(20)
as
begin
	delete from Attention
	where idpay = @id_pay and idgain = @id_gain
end

--获得所有付费用户信息
create proc getAllPayUser
as
begin
	select * from RegistedUser
	where type = 2
end

--获得个人所有关注
create proc getAttentionByUser
@id_pay char(20)
as
begin
	select * from RegistedUser
	where id in
		select idgain from Attention
		where idpay = @id_pay
end


--删除评论
create proc deleteComment
@id uniqueidentifier
as
begin
	delete from Comment
	where where id = @id
end

--获得用户样式文件物理路径
create proc getStylePathByUser
@id char(20)
as

begin
	select fileurl from ZoneStyle
	where id is
		select zonestyleid from RegistedUser
		where id = @id

return
end

--获得用户头像文件物理路径
create proc getPortraitPathByUser
@id char(20)
as
declare @@path char(500)
begin
	select portraitPath into @@path
	from RegistedUser
	where id = @id
	return @@path
end

--
create proc insertTag
@tag_id uniqueidentifier,
@commodity_id uniqueidentifier
as
begin
	insert into Tag
	values (@tag_id, 0)
	insert into TagAndCommodity
	values (@tag_id, @commodity_id)
end

------
create proc getTagByCommodity
@commodity_id uniqueidentifier
as
begin
	select * from Tag
	where id in
		select tagid from TagAndCommodity
		where commodityid = @commodity_id
end

create proc updateTag
@id char(20)
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


---$
---更新用户
create proc updateRegistedUser
@id char(20),
@pwd char(20),
@nickname char(20),
@email char(50),
@phone char(20),
@zonestyleid char(50),
@portraitPath char(500),
@city char(20),
@school char(20),
@address char(50),

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

--fufei shangjia
--得到所有投诉
create proc getAllComplaint
as
begin
	select * from Complaint
end


--得到所有留言?

create proc getUnreadPrivateMessageByUser
@user_id char(20)
as

begin
	select * from Message
	where userto = @user_id
		and type = 1
		and state = 1
end

--
create proc getUnreadPublicMessageByUser
@user_id char(20)
as

begin
	select * from Message
	where userto = @user_id
		and type = 2
		and state = 1
end

create proc getAllPublicMessageByUser
@user_id char(20)
as

begin
	select * from Message
	where userto = @user_id
		and type = 2
end

create proc getTagByCommodity
@commodity_id uniqueidentifier

as

begin
	select * from Tag
	where id in
	(select tagid from TagAndCommodity
	where commodityid = @commodity_id)
end
--还要一个商品返回tag
--