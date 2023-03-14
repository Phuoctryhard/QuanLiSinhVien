create table SV(
 Msv int primary Key Not null,
 Ten Nvarchar(50) not null,
 NgaySinh Date not null,
 Dtb float not null,
 Sex bit not null,
 Pic bit  not null,
 HocBa bit not null,
 Cccd bit not null,
 idLop varchar(50) not null,
 )
 insert into SV(Msv,Ten,NgaySinh,Dtb,Sex,Pic,HocBa,Cccd,idLop)
 values (102210225,'NgoDinhPhuoc','2022-11-11',10,1,1,1,1,'21Dt3'),
 (102210227,'Ngo','2022-11-11',10,1,1,1,1,'20Dt3'),
 (102210228,'NgoDinh','2022-11-11',10,1,1,1,1,'20Dt3')

 create table LopSinhHoat(
	idLop varchar(50) primary key not null,
	TenLop varchar(50) not null, 
)
insert into LopSinhHoat(idLop,TenLop)
values ('21Dt3','21TclcDt3 '),
('20Dt3','20TclcDt3')
select sv.Msv,sv.Ten,sv.NgaySinh,sv.Dtb,sv.Sex,sv.Pic,sv.HocBa,sv.Cccd,Lsh.TenLop from  LopSinhHoat as Lsh , SV as sv where Lsh.IdLop = sv.idLop;
select sv.Msv,sv.Ten,sv.NgaySinh,sv.Dtb,sv.Sex,sv.Pic,sv.HocBa,sv.Cccd,Lsh.TenLop from  LopSinhHoat as Lsh , SV as sv where Lsh.IdLop = sv.idLop and Lsh.TenLop ='20TclcDt3';
drop table SV,LopSinhHoat
Select COUNT(*) From SV Where Msv = '10221026';