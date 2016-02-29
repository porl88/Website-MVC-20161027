<%@ Page Language="C#" %>
<% Response.StatusCode = 500; %>
<% Server.Transfer("/errors/500.html"); %>