il.examples.patterns
====================

Beispielprojekt zum Vortrag vom 04.09.2013:  "Softwaredesign, das Zusammenspiel der Patterns"


##Die Slides
Hier sind die PPT Slides, dies ist ein direkter Link zu Skydrive Datei:
[.Net UserGroupe Dortmund-Vortrag.pptx](http://sdrv.ms/15vnNIg "Softwaredesign, das Zusammenspiel der Patterns")


Der Solutionfolder "Infrastructure" beinhaltet 3 optionale Repositories, 
diese können in der jeweiligen UnityConfig.cs registriert werden:

z.B. in der:
IL.Examples.Patterns.WebApplication\App_Start\UnityConfig.cs
oder in der :
IL.Examples.Patterns.Console\UnityConfig.cs

<pre>
    public static void RegisterTypes(IUnityContainer container) 
    {
        
        string connection = ConfigurationManager.ConnectionStrings["Default"].ConnectionString; 
        //container.RegisterType<IUserRepository, UserRepository>(new InjectionConstructor(connection));
        container.RegisterType<IUserRepository, XmlUserRepository>(new InjectionConstructor(container.Resolve<RootPath>()));
    
        //container.RegisterType<IUserRepository, EFUserRepository>();
    }
</pre>

Damit die Beispiele richtig funktionieren sollte nur ein Repository gleichzeit registriert werden!

<b>Die Zugangsdaten für die Webanwendung:</b>

usr: admin<br/>
pwd: admin


##Wichtig:
Das Konsolenprojekt "IL.Examples.Console" hat in der App.config einen festen Connectionstring

<pre>
  &lt;connectionStrings&gt;
    &lt;add name="Default" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=il-examples-patterns;Integrated Security=SSPI;AttachDBFilename=E:\DB\il-examples-patterns.mdf" providerName="System.Data.SqlClient" /&gt;
  &lt;/connectionStrings&gt;
</pre>

Dieser sollte durch eigenen ersetzt werden. Der Pfad ist relativ zum WebApplication projekt in derselben Solution!
Das WPF Projekt ist leider noch nicht fertiggeworden, das werde ich aber auf jeden Fall nachholen, wenn Einer Lust hat, der 
kann da gerne aushelfen.  

Die Webanwendung sollte am anfang mit dem EFRepository gestartet werden, dadurch wird die Datenbank mit den entsprechenden Daten angelegt. 
Anschließend kann man die Repositories wechseln.

##Weiteres:
fals jemand Interesse an einem weiteren DDD(DomainDrivenDesign) Beispiel-Projekt hat, hier ist es:
[DDDOnlineStore.Net](https://github.com/thecodemonkey/DDDOnlineStore.Net)
Bei diesem Projekt liegt der Focus vor allem auf der Kombination zwischen DDD und EntityFramework. 
Auch wenn ich die EF Implementierung mittlerweile etwas anders machen würde, ist dies ein möglcher Ansatz! 


