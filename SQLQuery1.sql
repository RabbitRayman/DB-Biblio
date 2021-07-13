Create database kurs1;
use kurs1;
-----------------------------------------------------------------
create table publication_type
(type_id int Primary KEY NOT NULL IDENTITY(1,1), type_name nvarchar(255) Unique not null);

create table countries
(country_id int Primary key not null IDENTITY(1,1),country_name nvarchar(255) Unique not null);

create table languages
(language_id int Primary key not null IDENTITY(1,1),language_name nvarchar(255) Unique not null);

create table publishers
(publisher_id int Primary Key Not null IDENTITY(1,1),publisher_name nvarchar(255) Unique not null, open_year int, publisher_director nvarchar(255), publisher_country int,foreign key (publisher_country) references countries(country_id));

create table authors
(author_id int Primary Key Not null IDENTITY(1,1),author_lastname nvarchar(255) not null,author_firstname nvarchar(255),author_middlename nvarchar(255), birthdate date, author_country int, author_publs int, foreign key (author_country) references countries(country_id));

create table publications
(publication_id int Primary Key Not null IDENTITY(1,1),publication_name nvarchar(255) not null,publication_type int,publication_year int, publisher int, pages int, publ_language int, foreign key (publication_type) references publication_type(type_id),foreign key (publisher) references publishers(publisher_id),foreign key (publ_language) references languages(language_id));

create table publication_author
(id int Primary Key Not null IDENTITY(1,1),author int, publication int, foreign key (author) references authors(author_id), foreign key (publication) references publications(publication_id));

CREATE UNIQUE INDEX publ_aut ON publication_author(author,publication);
-----------------------------------------------------------------
create procedure publtype_update (@id int, @name nvarchar(255))
AS
begin
update publication_type set type_name=@name where type_id = @id
end;

create procedure publtype_insert (@name nvarchar(255))
AS
begin
insert into publication_type (type_name) values (@name)
end;

create procedure publtype_delete (@id int)
AS
begin
delete from publication_type where type_id = @id
end;
-----------------------------------------------------------------
create procedure countries_update (@id int, @name nvarchar(255))
AS
begin
update countries set country_name=@name where country_id = @id
end;

create procedure countries_insert (@name nvarchar(255))
AS
begin
insert into countries (country_name) values (@name)
end;

create procedure countries_delete (@id int)
AS
begin
delete from countries where country_id = @id
end;
-----------------------------------------------------------------
create procedure languages_update (@id int, @name nvarchar(255))
AS
begin
update languages set language_name=@name where language_id = @id
end;

create procedure languages_insert (@name nvarchar(255))
AS
begin
insert into languages (language_name) values (@name)
end;

create procedure languages_delete (@id int)
AS
begin
delete from languages where language_id = @id
end;
-----------------------------------------------------------------
create procedure publishers_update (@id int, @name nvarchar(255),@year int,@director nvarchar(255), @country int)
AS
begin
update publishers set publisher_name=@name, open_year=@year, publisher_director=@director, publisher_country=@country where publisher_id = @id
end;

create procedure publishers_insert (@name nvarchar(255),@year int,@director nvarchar(255), @country int)
AS
begin
insert into publishers (publisher_name, open_year, publisher_director, publisher_country) values (@name, @year, @director, @country)
end;

create procedure publishers_delete (@id int)
AS
begin
delete from publishers where publisher_id = @id
end;
-----------------------------------------------------------------
create procedure authors_update (@id int, @lastname nvarchar(255),@firstname nvarchar(255),@middlename nvarchar(255),@birth date, @country int)
AS
begin
update authors set author_lastname=@lastname,author_firstname=@firstname,author_middlename=@middlename,birthdate=@birth, author_country=@country where author_id = @id
end;

create procedure authors_insert (@lastname nvarchar(255),@firstname nvarchar(255),@middlename nvarchar(255),@birth date, @country int)
AS
begin
insert into authors (author_lastname,author_firstname,author_middlename,birthdate, author_country,author_publs) values (@lastname,@firstname,@middlename,@birth, @country,0)
end;

create procedure authors_delete (@id int)
AS
begin
delete from authors where author_id = @id
end;
-----------------------------------------------------------------
create procedure publications_update (@id int, @name nvarchar(255),@publ_type int,@publ_year int,@publisher int, @pages int, @publ_language int)
AS
begin
update publications set publication_name=@name,publication_type=@publ_type,publication_year=@publ_year,publisher=@publisher,pages=@pages, publ_language=@publ_language where publication_id = @id
end;

create procedure publications_insert (@name nvarchar(255),@publ_type int,@publ_year int,@publisher int, @pages int, @publ_language int)
AS
begin
insert into publications (publication_name,publication_type,publication_year,publisher,pages, publ_language) values (@name,@publ_type,@publ_year,@publisher, @pages, @publ_language)
end;

create procedure publications_delete (@id int)
AS
begin
delete from publications where publication_id = @id
end;
-----------------------------------------------------------------
create procedure publ_auth_update (@id int, @author int, @publication int)
AS
begin
update publication_author set author=@author,publication=@publication where id = @id
end;

create procedure publ_auth_insert (@author int, @publication int)
AS
begin
insert into publication_author (author,publication) values (@author,@publication)
end;

create procedure publ_auth_delete (@id int)
AS
begin
delete from publication_author where id = @id
end;
------------------------------------------------------------------
CREATE INDEX lastname_index ON authors (author_lastname)
CREATE INDEX publisher_index ON publishers(publisher_name)
CREATE INDEX publication_index ON publications(publication_name)
-------------------------------------------------------------------
Create login user1 With password='user1'
CREATE USER UserGuest FOR LOGIN user1
GRANT SELECT ON dbo.authors TO UserGuest
GRANT SELECT ON dbo.countries TO UserGuest
GRANT SELECT ON dbo.languages TO UserGuest
GRANT SELECT ON dbo.publication_author TO UserGuest
GRANT SELECT ON dbo.publication_type TO UserGuest
GRANT SELECT ON dbo.publications TO UserGuest
GRANT SELECT ON dbo.publishers TO UserGuest
GRANT SELECT ON publ_language TO UserGuest
GRANT SELECT ON publ_pages TO UserGuest
GRANT SELECT ON publ_language1 TO UserGuest
GRANT SELECT ON auth_publs TO UserGuest
GRANT SELECT ON Publi TO UserGuest
GRANT SELECT ON publish_numb TO UserGuest
-------------------------------------------------------------------
Create login admin1 With password='admin1'
CREATE USER UserAdmin FOR LOGIN admin1
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.authors TO UserAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.countries TO UserAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.languages TO UserAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.publication_author TO UserAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.publication_type TO UserAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.publications TO UserAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.publishers TO UserAdmin;
GRANT SELECT ON publ_pages TO UserAdmin
GRANT SELECT ON publ_language1 TO UserAdmin
GRANT EXECUTE TO UserAdmin
GRANT SELECT ON auth_publs TO UserAdmin
GRANT SELECT ON Publi TO UserAdmin
GRANT SELECT ON publish_numb TO UserAdmin
-------------------------------------------------------------------
create function publ_pages()
returns table
AS
return select p.publication_name 'Название', a.author_lastname 'Фамилия автора',a.author_firstname 'Имя автора', a.author_middlename 'Отчетство/Среднее имя автора', p.pages, 
CASE
WHEN p.pages>=150 THEN 'Крупный объём'
WHEN p.pages>=50 THEN 'Средний объём'
ELSE 'Малый объём'
END 'Объём публикации'
from authors a INNER JOIN publication_author pa ON a.author_id=pa.author INNER JOIN publications p ON pa.publication=p.publication_id GROUP BY p.publication_id,p.publication_name,a.author_id,a.author_lastname,a.author_firstname,a.author_middlename,p.pages
-----------------------------------------------------------------
create view Publi
AS
select p.publication_name 'Название',pt.type_name 'Тип',p.publication_year 'Год выпуска', ps.publisher_name 'Издатель', p.pages 'Кол-во страниц', l.language_name 'Язык' FROM publications p INNER JOIN languages l ON p.publ_language=l.language_id INNER JOIN publication_type pt ON p.publication_type=pt.type_id INNER JOIN publishers ps ON p.publisher=ps.publisher_id 
-----------------------------------------------------------------
CREATE TRIGGER logtrigins
ON publication_author
AFTER INSERT,UPDATE, DELETE
AS
DECLARE cur CURSOR FOR SELECT COUNT(*),author FROM publication_author GROUP BY author
DECLARE @num int
DECLARE @aut int
OPEN cur
FETCH NEXT FROM cur INTO @num, @aut
WHILE @@FETCH_STATUS = 0
BEGIN
UPDATE authors set author_publs=@num WHERE author_id=@aut
FETCH NEXT FROM cur INTO @num,@aut
END
CLOSE cur
DEALLOCATE cur
-----------------------------------------------------------------
create function auth_publs()
returns table
AS
return SELECT author_lastname 'Фамилия',author_firstname 'Имя', author_middlename 'Отчетство/Среднее имя', author_publs 'Количество публикаций'
FROM authors a1
WHERE a1.author_publs>ALL(
                      SELECT a2.author_publs
                      FROM authors a2
                      WHERE a2.author_id<>a1.author_id
                    )
-----------------------------------------------------------------				
create function publ_language1 (@lang nvarchar(255),@num int)
returns table
AS
return select a.author_lastname 'Фамилия',a.author_firstname 'Имя', a.author_middlename 'Отчетство/Среднее имя', c.country_name 'Страна', COUNT(*) 'Количество публикаций на языке', dbo.procent(a.author_publs,COUNT(*)) 'Процент от общего числа публикаций' from countries c INNER JOIN authors a ON c.country_id=a.author_country INNER JOIN publication_author pa ON a.author_id=pa.author INNER JOIN publications p ON pa.publication=p.publication_id INNER JOIN languages l ON p.publ_language=l.language_id WHERE l.language_name=@lang GROUP BY a.author_id,a.author_lastname,a.author_firstname,a.author_middlename,c.country_name,a.author_publs HAVING COUNT(*)>=@num
-----------------------------------------------------------------	
create function procent(@allpubl int, @partpubl int) 
returns float
AS
BEGIN
  return 100*(cast(@partpubl as real)/cast(@allpubl as real));
END
-----------------------------------------------------------------	
create procedure coun_lang_tr(@coun nvarchar(255),@lang nvarchar (255))
as
begin
	BEGIN TRANSACTION
	BEGIN TRY
		insert into countries (country_name) values (@coun);
		insert into languages (language_name) values (@lang);
		COMMIT;
	END TRY
	BEGIN CATCH
	IF @@ERROR <> 0 ROLLBACK;
	END CATCH
end
-----------------------------------------------------------------
create function func1(@id int)
returns table
AS
return SELECT * FROM countries WHERE country_id=@id

create procedure proc1(@id int)
AS
begin
SELECT publisher_name 'Издатель', (select country_name from func1(publisher_country)) 'Страна' FROM publishers WHERE publisher_id=@id
end
-----------------------------------------------------------------
SELECT * FROM authors a WHERE a.author_id in (SELECT publisher_id FROM publishers p)
-----------------------------------------------------------------
SELECT p.publication_name, 
(SELECT author_lastname
FROM authors a LEFT OUTER JOIN countries c ON (a.author_country  = c.country_id) WHERE c.country_id=1 and a.author_id=c.country_id) lastname 
FROM publications p
-----------------------------------------------------------------
SELECT MAX(author_publs) 
FROM (SELECT a.author_lastname, a.author_firstname, a.author_publs FROM authors a) tmp_d
-----------------------------------------------------------------
SELECT publication_year
FROM publications p
WHERE publication_year > (SELECT MAX(open_year) FROM publishers ps  WHERE  ps.publisher_id=p.publisher)
-----------------------------------------------------------------
SELECT c.country_id,c.country_name, 
(SELECT COUNT(ps.publisher_id)
FROM publishers ps 
WHERE ps.publisher_country=c.country_id) publishers
FROM countries c
-----------------------------------------------------------------
SELECT p.publication_id, p.publication_name, p.pages
FROM publications p JOIN 
(SELECT  pa.publication, count(*) md  
FROM publication_author pa 
GROUP BY pa.publication) pa_1 ON p.publication_id=pa_1.publication