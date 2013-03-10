--3

create table Message
(
	id uniqueidentifier primary key,
	userfrom nvarchar(50),
	userto nvarchar(50),

	content nvarchar(500),
	state int default 1, -- 0 == read, 1 == unread
	type int default 1, -- 1 == private, 2 == public

	foreign key (userfrom) references RegistedUser(id) on delete cascade,
	foreign key (userto) references RegistedUser(id)
)

/*
create table MessageState
(
	read uniqueidentifier,
	unread uniqueidentifier,

	foreign key (read) references Message(id) on delete cascade
	foreign key (unread) references Message(id) on delete cascade
)
*/

create table Comment
(
	id uniqueidentifier primary key,
	userfrom nvarchar(50),
	userto nvarchar(50),

	content nvarchar(500),

	state int, --0 == read, 1 == unread
	commodityid uniqueidentifier,


	foreign key (userfrom) references RegistedUser(id), --on delete cascade,
	foreign key (userto) references RegistedUser(id),-- on delete cascade,

	foreign key (commodityid) references Commodity(id) on delete cascade
)

create table Complaint
(
	id uniqueidentifier primary key,
	userfrom nvarchar(50),
	userto nvarchar(50),

	content nvarchar(500),

	state int, -- 0==read, 1==unread
	commodityid uniqueidentifier,


	foreign key (userfrom) references RegistedUser(id), --on delete cascade,
	foreign key (userto) references RegistedUser(id), --on delete cascade,

	foreign key (commodityid) references Commodity(id) on delete cascade
)