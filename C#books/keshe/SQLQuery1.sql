use library
create table admin      /*����Ա��*/
(
	aId nvarchar(20) not null primary key,
	aPwd nvarchar(20) not null,
	aName nvarchar(20) not null,
	aGender nvarchar(1) not null,
	aPhoNum nvarchar(11) not null,
	aPower int not null,
	aBan int not null
)

create table users    /*�û���*/
(
	uId nvarchar(20) not null primary key,
	uPwd nvarchar(20) not null,
	uName nvarchar(20) not null,
	uGender nvarchar(1) not null,
	uPhoNum nvarchar(11) not null,
	uBan int not null
)

create table bookclass   /*ͼ������*/
(
	bcNum int IDENTITY(1,1) not null primary key,     /*������������*/
	bkC nvarchar(10) not null
)

create table books      /*�鼮��*/
(
	bookId int IDENTITY(1,1) not null primary key,   /*������������*/
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

create table orderinfo      /*������Ϣ��*/
(
	orderNum int IDENTITY(1,1) not null primary key,       /*������������*/
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

/*���ͼ�����*/
insert into bookclass(bkc) values('�����')
insert into bookclass(bkc) values('����ѧ')
insert into bookclass(bkc) values('����ѧ')
insert into bookclass(bkc) values('�Ļ�')
insert into bookclass(bkc) values('�����ѧ')
insert into bookclass(bkc) values('����ѧϰ')
/*���ͼ����Ϣ*/
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('�������','Jon��Bentley','�����ʵ������','978-7-115-35761-8','42','39','2000','���˼��',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('C������ȱ��','Andrew��Koeing','�����ʵ������','978-7-115-17179-5','35','30','2000','C����',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('Cר�ұ��','Peter Van Der Linden','�����ʵ������','978-7-115-17108-1','45','42','2000','C����',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('Python�������ݲɼ�','Ryan Mitchell','�����ʵ������','978-7-115-41629-2','59','55','2000','Python���翪��',1)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('����̸������ѧ','Rrica Ariel Fox','�й�������湫˾','978-7-5057-3422-7','49.8','44','2000','̸������ѧ',3)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('�������','�ƽ�','���������','7-01-006006-1','56','50','2000','�й�����',4)
insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bcNum) values('�����˼ά','Rob Koplowitz','��ҵ���������','978-7-5164-1050-9','39.8','35','2000','��ҵ����',2)
/*��ӹ���Ա��Ϣ*/
insert into admin(aId,aPwd,aName,aGender,aPhoNum,aPower,aBan) values('root','123456','admin','��','15552235129',1,0)
insert into admin(aId,aPwd,aName,aGender,aPhoNum,aPower,aBan) values('1001','123','����','��','19906408816',0,0)
insert into admin(aId,aPwd,aName,aGender,aPhoNum,aPower,aBan) values('1002','123','С��','Ů','18653590144',0,0)
/*����û���Ϣ*/
insert into users(uId,uPwd,uName,uGender,uPhoNum,uBan) values('2001','123','����','��','13152265229',0)
insert into users(uId,uPwd,uName,uGender,uPhoNum,uBan) values('2002','123','��Դ','Ů','19906408816',1)
