--2

create table Commodity
(
	id uniqueidentifier primary key,
	userfrom nvarchar(50),
	name nvarchar(50),

	discription nvarchar(500),
	starttime datetime,
	endtime datetime,
	price float,
	picturepath nvarchar(500),
	--
	kind int,
	popularity int,

	foreign key (userfrom) references RegistedUser(id) on delete cascade
)

create table DemandCommodity
(
	id uniqueidentifier primary key,
	userfrom nvarchar(50),
	name nvarchar(50),

	discription nvarchar(500),
	starttime datetime,
	endtime datetime,
	price float,
	--
	kind int,
	popularity int,
	foreign key (userfrom) references RegistedUser(id) on delete cascade
)

create table Tag
(
	id nvarchar(50) primary key,
	popularity int
)

create table TagAndCommodity
(
	tagid nvarchar(50),
	commodityid uniqueidentifier,

	primary key (tagid, commodityid),
	foreign key (tagid) references Tag(id) on delete cascade,
	foreign key (commodityid) references Commodity(id) on delete cascade
)

create table CommodityOrder
(
	id int primary key identity,
	userfrom nvarchar(50),
	userto nvarchar(50),
	state int default 1 -- 0 == read, 1 == unread
)

create table OrderAndCommodity
(
	orderid int,
	commodityid uniqueidentifier,

	primary key (orderid, commodityid),
	foreign key (orderid) references commodityOrder(id) on delete cascade,
	foreign key (commodityid) references Commodity(id) on delete cascade
)

create table Collect
(
	userid nvarchar(50),
	commodityid uniqueidentifier,

	primary key (userid, commodityid),
	foreign key (userid) references RegistedUser(id) on delete cascade,
	foreign key (commodityid) references Commodity(id) --on delete cascade
)