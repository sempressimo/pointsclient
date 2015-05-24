<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Points_Web.login" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
<head runat="server">
    <title>Autocentro Toyota Rewards</title>
    
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery-ui.structure.css" />
    <link rel="stylesheet" type="text/css" href="css/top.css" />
    <link rel="stylesheet" type="text/css" href="css/modal.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/form.css" />
    <link rel="stylesheet" type="text/css" href="css/mygrid.css" />
    <link rel="stylesheet" type="text/css" href="css/dashboard.css" />
    <link rel="stylesheet" type="text/css" href="css/calendar.css" />
	<link rel="stylesheet" type="text/css" href="css/login.css" />
    <script type="text/javascript" src="Scripts/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,800,300' rel='stylesheet' type='text/css'>
	<link rel="stylesheet" type="text/css" href="css/normalize.css" />

	<link rel="stylesheet" type="text/css" href="css/set1.css" />
	<!--[if IE]>
  	<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    
    <style type="text/css">
		
		.girldiv {
			border:0px solid red;
			width:auto;
			display:inline;
			position:absolute;
			top:-59px;
			right:-150px;
			z-index: 10;
		}
		
		.h300 {
			height:320px;
		}
		
		.h250 {
			height:270px;
		}
		
		.mainwrapper 
		{
			margin:10% auto;
			width:60%;
		}
		
		.wrapper {
			margin:auto;
		}
		
		.footer li {
			list-style-type:none;
		}
		
		.footer li a {
			text-decoration:underline;
			color:#fff;
		}
		
		.mainbg {
			background:url(images/cover.jpg);
			background-size:cover;
		}
		
		.staffbg {
			background:url(images/staff.jpg);
			background-size:cover;
		}
		
		.logobg {
			background:url(images/logobg.jpg);
			background-size:cover;
		}
		
		.loginbg {
			background:url(images/loginbg.jpg);
			background-size:cover;
		}
		
		.alignright {
			text-align:right;
		}
		
		.aligncenter {
			text-align:center;
            top: 0px;
            left: 0px;
        }
		
		a.catalogtitle {
			position:absolute;
			z-index:3;
			left:10px;
			bottom:10px;
			font-size:62px;
			color:#fff;
			text-align:center;
			text-shadow: 0px 1px 20px #000;
			-webkit-transition:background 2s;
			transition:background 2s;
		}
		
		a.catalogtitle:hover {
			text-decoration:none;
			color:yellow;
			
		}
		
	</style>
</head>
<body>
<form runat="server">

<div class="topwrapper">
    	<div class="wrapper half">
        	<a href="#" class="left"><img src="images/logo.png"></a>
            <ul class="nav nav-pills pull-right whitetext">
                    
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">Mi cuenta <span class="caret"></span></a>
                        <ul class="dropdown-menu white" role="menu">
                            <li class="dropdown whitetext">
                                <a href="#">Area de cliente del dealer</a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="#">Catalogo</a>
                    </li>
                    
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" >Administracion <span class="caret"></span></a>
                        <ul class="dropdown-menu white" role="menu">
                            <li class="dropdown whitetext">
                                <a href="#">Administrar catalogo</a>
                                <a href="#">Administrar categorias de premios</a>
                            </li>
                        </ul>
                    </li>

                </ul>
                
            <div class="cleaner"></div>
        </div>
    </div>
	
    <div class="mainwrapper shadow">
		<div class="row">
			<div class="col-md-7 mainbg h300">
            	<img alt="" src="images/girl.png" class="girldiv" />
            </div>
			<div class=" col-md-5 loginbg h300">
            	
                <ul class="login-detail">
                	<li><h2>Clientes actuales</h2></li>
                    <li><input id="txtUser" runat="server" type="text" placeholder="Provea su telefono"/>
                    </li>
                    <li><input id="txtPassword" runat="server" type="text" placeholder="Provea su contraseña"/></li>
                    <li>
                        <asp:Button ID="cmdLogin" runat="server" Text="Entrar" 
                            onclick="cmdLogin_Click" />
                    </li>
                    <li><a href="">No recuerda su contraseña?</a></li>
                    <li>
                        <asp:Label ID="lblFeedBack" ForeColor="Orange" runat="server" Text=""></asp:Label></li>
               </ul>
            </div>
		</div>
		<div class="row">
			<div class="col-md-6 staffbg h250">
            	
                	<a href="#" class="catalogtitle"><img alt="" src="images/dialog.png" /><span>Catalogo</span></a>
            </div>
			<div class="col-md-6 logobg h250"></div>
		</div>
	</div>
    
    <div class="blackclear whitetext footer">
    	<div class="row">
        	<div class="wrapper half">
                <div class="col-md-4 "><img alt="" src="images/logo.png"/></div>
                <div class="col-md-4 aligncenter">
                	<ul>
                    	<li>&copy; Autocentro Toyota</li>
                        <li>Todos los derechos reservados</li>
                    </ul>
                </div>
                <div class="col-md-4 alignright">
                	<ul>
                    	<li><a href="#">Terminos y condiciones</a></li>
                        <li><a href="#">Politicas & seguridad</a></li>
                    </ul>
                </div>
           </div>
        </div>
    </div>
</form>
</body>
</html>
