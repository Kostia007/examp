select count(*)  as `Количество ДТП совершенные в нетрезвом состоянии`
 from дтп where Причина='Водитель в нетрезвом состоянии';
 select counting() as `Доля таких проишествий(%)`;
 
 delimiter //
CREATE FUNCTION counting() returns int
BEGIN
DECLARE n,t,c float;
SELECT count(*) INTO t FROM дтп where Причина='Водитель в нетрезвом состоянии';
SELECT count(*) INTO n FROM дтп;
SELECT t/n*100 INTO c;
return c;
END//