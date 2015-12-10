<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ParagraphAnalyser.Web.WebForms.Main" %>

<%@ Import Namespace="ParagraphAnalyser.Core" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/site.css" rel="stylesheet" />
    <script src="/Scripts/modernizr-2.6.2.js"></script>
    <title></title>
</head>
<body>
<div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/">Paragraph Analyser</a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li><a href="/">MVC Example</a></li>
                    <li><a href="/WebForms/Main.aspx">Web Forms Example</a></li>
                </ul>
            </div>
        </div>
    </div>

    <div class="container body-content">

        <div class="jumbotron">
            <h1>Paragraph Analyser</h1>
            <p class="lead">Analyse a paragraph of text using ASP.Net Web Forms</p>
        </div>

        <form id="form1" runat="server">
            <div>
                <p class="lead">Enter comma delimited chars for Words</p>
                <asp:TextBox runat="server" ID="charsForWords" name="charsForWords" Style="min-width: 100%">r,e,g,i,s,t,e,r</asp:TextBox>
                <p class="lead">Enter comma delimited chars for Sentences</p>
                <asp:TextBox runat="server" ID="charsForSentences" name="charsForSentences" Style="min-width: 100%">n,o,w</asp:TextBox>
                <p class="lead">Enter paragraph to analyse</p>
                <asp:TextBox runat="server" ID="paragraph" name="paragraph" Style="min-width: 100%" TextMode="MultiLine">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at ipsum sapien. Ut risus libero, volutpat in nulla a, vestibulum accumsan tellus. Mauris condimentum ante lorem, sed viverra lacus fringilla sit amet. Pellentesque aliquet mauris at enim mollis pulvinar. Phasellus pulvinar enim nec lorem ultricies cursus. Nulla erat felis, ultrices.</asp:TextBox>
                <p class="lead">
                    Ignore Case Sensitivity
            <asp:CheckBox runat="server" type="checkbox" ID="ignoreCase" name="ignoreCase" class="chkbx" value="true" />
                </p>
                <asp:Button runat="server" Text="Process" />
            </div>
        </form>
        <% if (Result != null)
            { %>
        <div>
            <h2>Words</h2>
            <table>
                <thead>
                    <tr>
                        <td>Letter</td>
                        <td>Quantity</td>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var item in Result.WordGroupedData.OrderedMatchingAndNotFound())
                        { %>
                    <tr>
                        <td><%= item.FirstChar %></td>
                        <td><%= item.Items.Count() %></td>
                    </tr>
                    <% } %>

                    <tr>
                        <td>Total That Match</td>
                        <td><%= Result.WordGroupedData.SumOfMatching()%></td>
                    </tr>
                    <tr>
                        <td>Total Unique Leading Chars</td>
                        <td><%= Result.WordGroupedData.Found().Count()%></td>
                    </tr>
                    <tr>
                        <td>Total All Words</td>
                        <td><%= Result.WordGroupedData.SumOfFound()%></td>
                    </tr>

                </tbody>
            </table>

            <h2>Sentences</h2>
            <table>
                <thead>
                    <tr>
                        <td>Letter</td>
                        <td>Quantity</td>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var item in Result.SentenceGroupedData.OrderedMatchingAndNotFound())
                        { %>
                    <tr>
                        <td><%= item.FirstChar %></td>
                        <td><%= item.Items.Count() %></td>
                    </tr>
                    <% } %>

                    <tr>
                        <td>Total That Match</td>
                        <td><%= Result.SentenceGroupedData.SumOfMatching()%></td>
                    </tr>
                    <tr>
                        <td>Total Unique Leading Chars</td>
                        <td><%= Result.SentenceGroupedData.Found().Count()%></td>
                    </tr>
                    <tr>
                        <td>Total All Sentences</td>
                        <td><%= Result.SentenceGroupedData.SumOfFound()%></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <% } %>
    </div>
    <script src="/Scripts/jquery-1.10.2.js"></script>
    <script src="/Scripts/bootstrap.js"></script>
</body>
</html>
