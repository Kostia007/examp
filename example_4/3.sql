select count_rozisk() as `Доля найденных машин(%)`;
 delimiter //
CREATE FUNCTION count_ROZISK() returns int
BEGIN
DECLARE n,t,c float;
SELECT count(*) INTO t FROM розыск where Результат='найдена';
SELECT count(*) INTO n FROM розыск;
SELECT t/n*100 INTO c;
return c;
END//