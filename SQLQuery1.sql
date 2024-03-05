CREATE DATABASE DBVENTA
use DBVENTA

CREATE TABLE Rol(
idRol int primary key identity(1,1),
nombre varchar(50),
fechaRegistro datetime default getdate()
)

CREATE TABLE Menu(
idMenu int primary key identity(1,1),
nombre varchar(50),
icono varchar(50),
url varchar(50)
)

CREATE TABLE MenuRol(
idMenuRol int primary key identity(1,1),
idMenu int references Menu(idMenu),
idRol int references Rol(idRol)
)

CREATE TABLE Usuario(
idUsuario int primary key identity(1,1),
nombreCompleto varchar(100),
correo varchar(40),
idRol int references Rol(idRol),
clave varchar(40),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

CREATE TABLE Categoria(
idCategoria int primary key identity(1,1),
nombre varchar(50),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

CREATE TABLE Producto(
idProducto int primary key identity(1,1),
nombre varchar(100),
idCategoria int references Categoria(idCategoria),
stock int,
precio decimal(10,2),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

CREATE TABLE NumeroDocumento(
idNumeroDocumento int primary key identity(1,1),
ultimo_numero int not null,
fechaRegistro datetime default getdate()
)

CREATE TABLE Venta(
idVenta int primary key identity(1,1),
numeroDocumento varchar(40),
tipoPago varchar(50),
total decimal(10,2),
fechaRegistro datetime default getdate()
)

CREATE TABLE DetalleVenta(
idDetalleVenta int primary key identity(1,1),
idVenta int references Venta(idVenta),
idProducto int references Producto(idProducto),
cantidad int, 
precio decimal(10,2),
total decimal(10,2)
)
