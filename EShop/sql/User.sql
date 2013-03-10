--1

create table ZoneStyle
(
	id nvarchar(50) primary key,
	fileurl nvarchar(500),
)

create table RegistedUser
(
	id nvarchar(50) primary key,
	pwd nvarchar(50),

	nickname nvarchar(50),
	email nvarchar(50),
	phone nvarchar(50),

	zonestyleid nvarchar(50),
	portraitPath nvarchar(500),

	city nvarchar(50),
	school nvarchar(50),
	address nvarchar(50),

	type int default 1, -- 1 == nopay, 2 == pay
--
	payamount float,
	payendtime datetime,


	foreign key (zonestyleid) references ZoneStyle(id) on delete cascade
)

--fanxiangsuoying
--create table rigistedUserType
--(
--	payment nvarchar(50),
--	nupayment nvarchar(50),
--
--	foreign key (payment) references rigistedUser(id) on delete cascade,
--	foreign key (nupayment) references rigistedUser(id) on delete cascade
--)

create table Attention
(
	idpay nvarchar(50),
	idgain nvarchar(50),

	foreign key (idpay) references RegistedUser(id), --on delete cascade,
	foreign key (idgain) references RegistedUser(id) on delete cascade
)







