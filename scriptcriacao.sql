create database Unico;
use Unico;
CREATE TABLE Feiras(
		   ID int,
	LONGITUDE varchar(50), 
	 LATITUDE varchar(50),
	  SETCENS varchar(255),
		AREAP varchar(255), 
	  CODDIST varchar(255), 
	 DISTRITO varchar(255), 
   CODSUBPREF varchar(255), 
     SUBPREFE varchar(255), 
      REGIAO5 varchar(255),
      REGIAO8 varchar(255), 
   NOME_FEIRA varchar(255), 
     REGISTRO varchar(255),
   LOGRADOURO varchar(255), 
       NUMERO varchar(255), 
       BAIRRO varchar(255),
   REFERENCIA varchar(255)
);
drop table Feiras;
select * from feiras;