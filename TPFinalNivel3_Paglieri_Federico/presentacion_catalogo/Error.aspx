<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="presentacion_catalogo.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Contenido_CSS/Error.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="contenedor">
       
        <article class="contenedor_error">
            <p class="error_texto"></p>

            <%if (Session["errorMessage"] != null)%>
            <%{%>
               
                <script>
                    let pContent = document.querySelector(".error_texto");

                    pContent.textContent = "<%= errorMessage%>";
                </script>  
            <%} %>
              
        </article>
    </section>

</asp:Content>
