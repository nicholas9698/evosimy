use library
create table admin      /*管理员表*/
(
	aId nvarchar(20) not null primary key,
	aPwd nvarchar(20) not null,
	aName nvarchar(20) not null,
	aGender nvarchar(1) not null,
	aPhoNum nvarchar(11) not null,
	aPower int not null,
	aBan int not null
)

create table users    /*用户表*/
(
	uId nvarchar(20) not null primary key,
	uPwd nvarchar(20) not null,
	uName nvarchar(20) not null,
	uGender nvarchar(1) not null,
	uPhoNum nvarchar(11) not null,
	uBan int not null
)

create table bookclass   /*图书分类表*/
(
	bcNum int IDENTITY(1,1) not null primary key,     /*主键自增生成*/
	bkC nvarchar(10) not null
)

create table books      /*书籍表*/
(
	bookId int IDENTITY(1,1) not null primary key,   /*主键自增生成*/
	bookName nvarchar(20) not null,
	bookAuthor nvarchar(20) not null,
	bookPublish nvarchar(40) not null,
	bookISBN nvarchar(40) not null,
	bookOprice float not null,
	bookVprice float not null,
	bookSnum int not null,
	bookContent nvarchar(20) not null,
	bookDcontent nvarchar(40),
	bcNum int
)

create table orderinfo      /*订单信息表*/
(
	orderNum int IDENTITY(1,1) not null primary key,       /*主键自增生成*/
	uId nvarchar(20) not null,
	uPhoNum nvarchar(11) not null,
	uAddress nvarchar(100) not null,
	orderValue float not null,
	bookName nvarchar(20) not null,
	bookAuthor nvarchar(20) not null,
	bookPublish nvarchar(20) not null,
	bookISBN nvarchar(40) not null,
	bookNum int not null,
	orderState nvarchar(3) not null,
	orderMath nvarchar(10) not null,
	payMath nvarchar(10) not null,
	orderTime nvarchar(20)
)

/*添加图书分类*/
insert into bookclass(bkc) values('计算机')
insert into bookclass(bkc) values('管理学')
insert into bookclass(bkc) values('心理学')
insert into bookclass(bkc) values('文化')
insert into bookclass(bkc) values('外国文学')
insert into bookclass(bkc) values('语言学习')
/*添加图书信息*/
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('编程珠玑','Jon·Bentley','人民邮电出版社','978-7-115-35761-8','42','39','2000','编程思想',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('C陷阱与缺陷','Andrew·Koeing','人民邮电出版社','978-7-115-17179-5','35','30','2000','C语言',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('C专家编程','Peter Van Der Linden','人民邮电出版社','978-7-115-17108-1','45','42','2000','C语言',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('Python网络数据采集','Ryan Mitchell','人民邮电出版社','978-7-115-41629-2','59','55','2000','Python网络开发',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('哈佛谈判心理学','Rrica Ariel Fox','中国友谊出版公司','978-7-5057-3422-7','49.8','44','2000','谈判心理学',3)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('大国崛起','唐晋','人民出版社','7-01-006006-1','56','50','2000','中国复兴',4)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('麦肯锡思维','Rob Koplowitz','企业管理出版社','978-7-5164-1050-9','39.8','35','2000','企业管理',2)
/*添加管理员信息*/
insert into admin(aId,aPwd,aName,aGender,aPhoNum,aPower,aBan) values('root','123456','admin','男','15552235129',1,0)
insert into admin(aId,aPwd,aName,aGender,aPhoNum,aPower,aBan) values('1001','123','张三','男','19906408816',0,0)
insert into admin(aId,aPwd,aName,aGender,aPhoNum,aPower,aBan) values('1002','123','小红','女','18653590144',0,0)
/*添加用户信息*/
insert into users(uId,uPwd,uName,uGender,uPhoNum,uBan) values('2001','123','李四','男','13152265229',0)
insert into users(uId,uPwd,uName,uGender,uPhoNum,uBan) values('2002','123','王源','女','19906408816',1)
