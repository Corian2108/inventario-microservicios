# Sistema Inventarios y Ventas .NET, Angular y SQLServer

Se trata de un sistema de inventarios implementado bajo la arquitectura de microservicios, la sfunciones básicas del sistema son que nos permite crear productos nuevos, editar los existentes, visualizarlos e incluso borrarlos, de una forma clara directa y amigable con el usuario, así mismo el apartado de transacciones lleva el registro de las compras (ingresos a inventario) y ventas (salida de inventario) realizadas por el operario, alterando de forma automática el stock de cada producto y validando que el stock sea el suficiente para completar la transacción.

# Arquitectura

* Frontend: Angular 19 (standalone components)
* Backend: 
    * Microservicio de Productos: (.Net estructurado por capas + SQL Server)
    * Microservicio de Transacciones: (.Net estructurado por capas + SQL Server)
    * Comunicación sincrona vía APIs REST

# Funcionalidades:

Productos: 
    * CRUD
    * Gestión de Stock
    * Feedback al usuario

Transacciones:
    * Consulta por filtros dinámicos
    * Agregar Venta (salida de stock)
    * Agregar Compra (ingreso a stock)
    * Validación de Stock en ventas
    * Actualización automática de Stock
    * Visualizaicón de historial

# Requisitos

.NET SDK 8
Node.js 10
Angular CLI
SQL Server

# Ejecución del proyecto
1. Ejecutar los archivos SQL ubicados en la carpeta database de este proyecto, si surge algún error ejecuta por bloques
2. En Visual Studio (Backend): 
    * dotnet restore
    * dotnet run
3. En Visual Studio Code (Frontend).
    * npm i
    * ng serve

## El proyecto incluye evidencias de funcionamiento en la carpeta evidencia de este repositorio