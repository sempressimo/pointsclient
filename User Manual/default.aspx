<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="User_Manual._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">

    <div class="container">

        <h1>Cliente Windows</h1>
        
        <h3>Pantalla Principal</h3>

        <p>Cuando se abre el sistema esta es la primera pantalla que ve el usuario. (Figura 1)</p>

        <img class="img-responsive" src="images/main.png" alt="Chania"/>

        <figure style="color: gray;"><i>Figue 1 - Pantalla Principal</i></figure>

        <hr/>

        <h3>Área de Búsqueda</h3>

        <p>Lo más común es que primero se busque al cliente utilizando el número de teléfono. Se puede entrar el numero en la caja de texto de manera manual o utilizando el lector de tarjeta que está conectado a la computadora. Luego se presiona el botón de [Buscar Cliente] (Figura 2.)  Si el cliente existe en sistema su información será poblada en pantalla como se ve en la figura 3.</p>

        <img class="img-responsive" src="images/search.png" alt="Chania"/>

        <figure style="color: gray;"><i>Figure 2- Área de búsqueda </i></figure>

        <br/>

        <img class="img-responsive" src="images/main 2.png" alt="Chania"/>

        <figure style="color: gray;"><i>Figure 3 - Luego de buscar y encontrar un cliente </i></figure>

        <hr/>

        <h3>Sección de Datos del Cliente </h3>

        <p>En esta sección se muestran los datos demográficos del cliente. También su balance de puntos y su “Web PIN” (Figura 4). Este PIN es el que el cliente utiliza para entrar al portal Web de clientes.</p>

        <img class="img-responsive" src="images/cinfo.png" alt="Chania"/>

        <figure style="color: gray;"><i>Figure 4- Datos del cliente </i></figure>

        <hr/>

        <h3>Sección de Transacciones</h3>

        <p>
            Esta sección muestra las transacciones que ha hecho el cliente. También indica si la transacción fueron puntos regalados y el motivo del regalo. 
        </p>

        <img class="img-responsive" src="images/Transactions.png" alt="Chania"/>

        <figure style="color: gray;"><i>Figure 5- Transacciones del cliente </i></figure>

        <hr/>

        <h3>Sección de Premios Reclamados por el Cliente</h3>

        <p>
            Esta sección muestra los premios que ha reclamado el cliente. Los mismos pueden haber sido reclamados por el mismo cliente utilizando el portal Web de clientes, o que el operador se los reclamo utilizando este sistema. Si los puntos fueron pagados (i.e. que Auto Centro Toyota le dio el descuento en el servicio) la sección de pagado sale en verde. De lo contrario sale en rojo. (Figura 6).
        </p>

        <p>
            Abajo hay 3 botones para administrar los puntos. Registrar Puntos para añadir una transacción nueva, Obsequiar Puntos si el gerente desea esto para el cliente o Usar Puntos que es donde se reclaman los premios a nombre del cliente. 
        </p>


        <img class="img-responsive" src="images/claims.png" alt="Chania"/>

        <figure style="color: gray;"><i>Figure 6- Premios (Puntos) reclamados por el cliente y botones para administrar los mismos</i></figure>

        <hr/>

        <h3>Pantalla de Registrar Puntos</h3>

        <p>En esta pantalla se entran los detalles de la transacción. El sistema auto calcula los puntos según la venta a un 3% (o le que se configure en el sistema).</p>

        <img class="img-responsive" src="images/Register Points.png" alt="Chania"/>

        <figure style="color: gray;"><i>Figue 1 - Pantalla Principal</i></figure>

        <hr/>

    </div>

</asp:Content>
