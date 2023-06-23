----CREATE  TABLE marcas(
---- codmarca INT PRIMARY  KEY IDENTITY,
---- marca NVARCHAR(50)NOT NULL unique
----);

----ALTER  TABLE marcas ADD foto VARBINARY(max);
----sp_rename 'marcas.foto' , 'logotipo','COLUMN'; 

----INSERT INTO marcas (marca)VALUES('Ford'),('Fiat'),('Lancia'),('BMW');

----SELECT  * FROM marcas;

----CREATE TABLE modelos (
---- idmodelo INT IDENTITY PRIMARY KEY,
---- modelo NVARCHAR(120) NOT NULL UNIQUE,
---- cilindrada FLOAT,
---- potencia INT,
---- ano INT CHECK(ano BETWEEN 1900 AND YEAR(GETDATE())),
---- combustivel nvarchar(20) CHECK(combustivel LIKE 'gasolina' OR combustivel LIKE 'gasóleo' OR combustivel LIKE 'gás' OR combustivel LIKE 'Eléctrico')
----)

----ALTER TABLE modelos ADD codmarca INT FOREIGN KEY REFERENCES marcas(codmarca);


--CREATE TABLE codigospostais(

-- codigopostal NVARCHAR(8) PRIMARY KEY,
-- localidade NVARCHAR(120)

--);

----INSERT INTO codigospostais (codigopostal,localidade)VALUES
----('1200-100','Lisboa'),('1220-440', 'Amadora');

----SELECT  * FROM codigospostais;

--CREATE TABLE pessoas (
-- nif INT IDENTITY PRIMARY KEY,
-- nome NVARCHAR(120)NOT NULL,
-- datanasc DATE,
-- idade AS DATEDIFF(YEAR,datanasc,GETDATE()),
-- rua NVARCHAR(250),
-- codigopostal NVARCHAR(8) FOREIGN KEY REFERENCES codigospostais(codigopostal) 

--);
--SELECT * FROM codigospostais;
--INSERT INTO pessoas(nome,datanasc,rua,codigopostal)VALUES
--('José Cliente', '1990-01-01', 'Rua das Alegrias','1200-100'),
--('Maria Funcionária', '2000-06-01', 'Avenida 25 de Abril','1220-440');

--SELECT  * FROM pessoas;

--CREATE TABLE clientes(
-- cliid INT IDENTITY PRIMARY KEY,
-- nif INT FOREIGN KEY REFERENCES pessoas(nif) NOT NULL,
-- dataregisto DATE DEFAULT GETDATE(),
--)

--INSERT INTO clientes (nif)VALUES(1);

----CREATE TABLE empregados(
---- numemp INT IDENTITY PRIMARY KEY,
---- nif INT FOREIGN KEY REFERENCES pessoas(nif) NOT NULL,
---- posto  NVARCHAR(120),
---- salario smallmoney
----)

----INSERT INTO empregados(nif, posto,salario)VALUES(2,'Administrativa',1235.67);

----SELECT  * FROM pessoas p INNER JOIN  empregados e ON p.nif=e.nif;

--CREATE TABLE carros(
-- idcarro INT IDENTITY PRIMARY KEY,
-- matricula NVARCHAR(8)NOT NULL UNIQUE,
-- idmodelo INT FOREIGN KEY REFERENCES modelos(idmodelo),
-- foto NVARCHAR(250),
-- preco SMALLMONEY,
-- ano INT CHECK(ano BETWEEN 1900 AND YEAR(GETDATE()))

--);
--INSERT INTO carros (matricula,idmodelo,preco,ano)
--VALUES('mx-12-34',1,12000,2016),('mx-13-24',2,14000,2017);

--SELECT * FROM modelos;
--SELECT * FROM marcas;

--INSERT INTO modelos(modelo, cilindrada, potencia,ano,codmarca)
--VALUES('Ford Fiesta',1100, 23,2010, 1),('Ford Puma',1100, 27,2011, 1),('Fiat 500x',900,17,2000,2);

SELECT * FROM clientes;
SELECT * FROM empregados;
SELECT * FROM carros;

CREATE TABLE compras(
 cliid INT FOREIGN KEY REFERENCES clientes(cliid),
 numemp INT FOREIGN KEY REFERENCES empregados(numemp),
 idcarro INT FOREIGN KEY REFERENCES carros(idcarro),
 data DATE DEFAULT GETDATE(),
 preco SMALLMONEY,
 CONSTRAINT pkcompra PRIMARY KEY(numemp,idcarro),
 CONSTRAINT ucclientecarro UNIQUE(cliid,idcarro)

);

INSERT INTO compras(cliid,numemp,idcarro,preco)VALUES(1,1,2,17500);

SELECT * FROM compras;

SELECT m.modelo, c.preco, c.data,s.nome cliente , p.nome empregado FROM compras c INNER JOIN empregados e ON e.numemp = c.numemp 
INNER JOIN pessoas p ON p.nif = e.nif 
INNER JOIN clientes cl ON cl.cliid=c.cliid
INNER JOIN pessoas s ON cl.nif=s.nif
INNER JOIN carros v ON v.idcarro= c.idcarro
INNER JOIN modelos m ON m.idmodelo= v.idmodelo;