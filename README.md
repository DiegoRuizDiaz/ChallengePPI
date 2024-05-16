# API de Órdenes
Esta API proporciona endpoints para administrar órdenes en un sistema de inversiones.
Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre las órdenes.

## Instalacion
Para poder operar con la API es necesario crear y configurar la conexion a la base de datos:

1. Abrir el archivo "appsettings.json" en la raíz del proyecto APIRest.
2. Busque la sección "ConnectionStrings"
3. Modifique la cadena de conexión "ConnectionString" según sea necesario para apuntar a su propia base de datos SQL Server. (Conexión,Servidor,Autenticación) ej:
   
   "ConnectionStrings": {
   
  "ConnectionString": "Server=nombre_de_su_servidor\\nombre_de_su_instancia;Database=INVERSION;Integrated Security=true"
  
}

5. Ejecutar en la base SQL que usará, el script para la creación de la base de datos ubicado en ChallengePPI\DBScript\DB_Script.sql 

## Endpoints

#### POST /auth/authenticate
Retorna un "bearer + token" que permite autenticarse.

Ejemplo de solicitud:

/auth/authenticate 	

{
  "nombre": "admin",
  "email": "admin@gmail.com"
}

#### GET /ordenes
Retorna todas las órdenes almacenadas en la base de datos.

Ejemplo de solicitud:

/ordenes

#### GET /ordenes/{OrdenId}
Retorna los detalles de una orden identificada por su ID.

Ejemplo de solicitud:

/ordenes/1

#### POST /ordenes
Crea una nueva orden. 

El campo "Precio" de la orden dependera del tipo de activo que este asociado al "Ticker" enviado.

Si es "Bono" solicitará un precio.Si es "Accion" no aceptará que envíen un precio. Si es "FCI" no afectara si se envia o no ya que utilizará el de DB.

Ejemplo de solicitud:

/ordenes

{
  "idCuenta": 1,
  
  "ticker": "GD30",
  
  "nombreActivo": "Inversion en Bonos Globales",
  
  "cantidad": 700,
  
  "precio": 1234.1234,
  
  "operacion": "Compra"
}

#### PUT  /ordenes/orden/{ordenId}/estado/{estado}
Actualiza el campo "Estado" de una orden existente identificada por su ID.

Esta actualización depedenderá del estado actual de la solicitud a actualizar.Si se encuentra en estado 1(Ejecutada) o 2(Cancelada) la misma se considera finalizada y no podra actualizarse.

Ejemplo de solicitud:

/ordenes/orden/1/estado/1 	o	/ordenes/orden/1/estado/Ejecutada

#### DELETE 	/ordenes/{ordenId}
Elimina una orden existente identificada por su ID.

Ejemplo de solicitud:

/ordenes/1
