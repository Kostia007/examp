use mydb;
call vladel('AB6846TE');

delimiter //
CREATE Procedure vladel(in t text)
BEGIN
DECLARE n int;
select count(*) into n from `номера_часники` where `гос_номер`= t;
if n=1 then select * from `номера_часники` where `гос_номер`= t;
else select * from `номера_юрлица` where `гос_номер`= t;
end if;
end//
