drop database dbloja;

/*CRIANDO O BANCO DE DADOS*/
create database dbLoja;
/*USANDO BANCO DE DADOS*/
use dbLoja;


/*CRIANDO AS TABELAS DO BANCO DE DADOS*/

create table tbLogin(
codLogin int primary key auto_increment,
usuario varchar(40),
senha varchar(20)
);


create table tbCliente(
Codigo int primary KEY auto_increment,
Nome varchar(40),
Telefone varchar(20),
Email varchar (40)
);

/*INSERINDO DADOS NA TABELA*/
insert into tbCliente(Codigo, Nome, Telefone, Email) values(2, "Huginho", "(11) 98363-7453", "huginho@gmail.com");
insert into tbCliente(Nome, Telefone, Email) values("Luizinho", "(11) 98363-7453", "huginho@gmail.com");
insert into tbLogin(usuario,senha) values ("Admin","12345");

/*CONSULTANDO AS TABELAS DO BANCO*/
-- describe tbCliente (UMA OUTRA OPÇÃO)
select * from tbCliente;
select * from tbLogin;


/*ALTERANDO AS TABELAS DO BANCO*/
alter table tbCliente add Teste varchar(20); 

/*REMOVENDO UMA COLUNA DAS TABELAS BANCO DE DADOS*/
alter table tbCliente drop column Teste;

/*ALTERANDO O VALOR DO ATRIBUTO*/
update tbCliente set Nome = "Zezinho" where Codigo = 1;

/*DELETANDO UMA INFORMAÇÃO (LINHA) INSERIDA NA TABELA*/
delete from tbCliente where Codigo = 2;