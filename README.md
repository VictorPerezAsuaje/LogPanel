# Configurable LogPanel

This solution is composed of three projects: 

## LogPanel

This is the project example. It simulates how you may want to integrate the system with your current project. There are a few things you may want to check here:

### How to configure your Program.cs

If you go to the Program.cs, I added two of my own local databases created for this purpose as a reference. The LogPanelEntities contains a Middleware to easily configure the clients with the databases from which you want to extract logs. I would suggest you fill the properties Name and Database at least. The Database property also allows you to set the column names of your table, you have 5 overridable columns: ColNameForId, ColNameForLogType, ColNameForTime, ColNameForMessage and ColNameForStacktrace, but only ColNameForId is required (it will throw an exception if you don't specify it).

In the configuration, each of the local databases added have its own configuration, one uses the Client class that comes with the library (LogPanelEntities) and the other extends the base class in order to create the CustomClient to add the CustomDescription property. This is meant to show you that you don't need to fully comply with the basic properties the library offers / requires.

### Integrating the Panels in your Controller / Views

Once configured the Program.cs, you can opt to integrate the LogPanelViews routing it to the Log area Exceptions controller, or create your own custom panel with the aesthetic and data you prefer. Both of those options are contained in the example the Classic and the Custom. Despite being really similar in desing (sorry, I'm that lazy), you can see that there are a few differences between them in terms of design and functionality. In fact, the Custom one only requires the Library, whereas the Classic requires both the Library and the Views.

If you are going to use some of the partials offered by the views library, I would strongly suggest you add in your Layout the following lines:

// In the <head> tag
@await RenderSectionAsync("Styles", required: false)

// Belog your last <script> tag but before the </body> tag
@await RenderSectionAsync("Scripts", required: false)

That way you can easily add the scripts and styles in the view you want. You can see that in the CustomLogger view:

@section Styles{
    <link rel="stylesheet" href="~/_content/LogPanelViews/css/exceptions.css" />
}

## LogPanelEntities

This project contains all the entities required for the LogPanel and LogPanelViews to work. You will most likely only use the LogPanelConfiguration for its AddClientLogConfig method, however you can extend the following classes as well to fit your needs: BaseClient and BaseDatabase. 

## LogPanelViews

Contains predefined views that make use of LogPanelEntities to create a log panel with the configuration you specified in your Program.cs. You can use this project also as an example of use for your own.

