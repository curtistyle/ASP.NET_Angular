INSERT INTO Rol(nombre) VALUES
('Administrador'),
('Empleado'),
('Supervisor')

INSERT INTO Usuario(nombreCompleto, correo, idRol, clave) VALUES
('codigo estudiante', 'code@example.com', 1, '123')

INSERT INTO Categoria(nombre, esActivo) VALUES
('Laptos',1),
('Monitores',1),
('Teclados',1),
('Auriculares',1),
('Memorias',1),
('Accesorios',1)

INSERT INTO Producto(nombre,idCategoria,stock,precio,esActivo) VALUES
('Laptop Samsung Book Pro',1,20,2500,1),
('Laptop Lenovo Idea Pad',1,30,2200,1),
('Laptop Asus Zenbook Duo',1,30,2100,1),
('Monitor Teros Gaming te-2',2,25,1050,1),
('Monitor Samsung Curvo',2,15,1400,1),
('Monitor Huawei Gamer',2,10,1350,1),
('Teclado Seisen Gamer',3,10,800,1),
('Teclado Antryx Gamer',3,10,1000,1),
('Teclado Logitech',3,10,1000,1),
('Auricular Logitech Gamer',4,15,800,1),
('Auricular Hyperx Gamer',4,20,680,1),
('Auricular Redragon RGB',4,25,950,1),
('Memoria Kingston RGB',5,10,200,1),
('Ventilador Cooler Master',6,20,200,1),
('Mini Ventilador Lenono',6,15,200,1)

INSERT INTO Menu(nombre,icono,url) VALUES
('DashBoard','dashboard','/pages/dashboard'),
('Usuarios','group','/pages/usuarios'),
('Productos','collections_bookmark','/pages/productos'),
('Venta','currency_exchange','/pages/venta'),
('Historial Ventas','edit_note','/pages/historial_venta'),
('Reportes','receipt','/pages/reportes')

--Menus para administrador
INSERT INTO MenuRol(idMenu,idRol) VALUES
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1)

--Menus para empleado
INSERT INTO MenuRol(idMenu,idRol) VALUES
(4,2),
(5,2)

--Menus para supervisor
INSERT INTO MenuRol(idMenu,idRol) VALUES
(3,3),
(4,3),
(5,3),
(6,3)


INSERT INTO numerodocumento(ultimo_Numero,fechaRegistro) VALUES
(0,getdate())