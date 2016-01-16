namespace MVC.Core.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Text;
    using Entities.Culture;
    using MVC.Core.Data.EntityFramework;
    using MVC.Core.Entities.Article;

    // http://www.codeproject.com/Tips/814618/Use-of-Database-SetInitializer-method-in-Code-Firs

    public class DatabaseInitializer : DropCreateDatabaseAlways<WebsiteDbContext>
    {
        protected override void Seed(WebsiteDbContext context)
        {
            this.CreateLanguages(context);
            this.CreateArticles(context);

            //context.Pages.Add(new Page());

            //context.PageVersions.Add(new PageVersion
            //{
            //    PageId = 1,
            //    LanguageCode = "en-gb",
            //    Title = "Home Page",
            //    Description = "Whatever",
            //    Keywords = "XXXX"
            //});

            //context.PageVersions.Add(new PageVersion
            //{
            //    PageId = 1,
            //    LanguageCode = "de-de",
            //    Title = "Achtung!",
            //    Description = "Nein! Nein!",
            //    Keywords = "kraftwerk"
            //});

            //context.PlainTexts.Add(new PlainText
            //{
            //    PageId = 1,
            //    PageVersionId = 1,
            //    Text = "Here is the home page title!"
            //});

            //context.PlainTexts.Add(new PlainText
            //{
            //    PageId = 1,
            //    PageVersionId = 2,
            //    Text = "Bonjour, ici c'est la page d'acceuil!"
            //});

            //context.RichTexts.Add(new RichText
            //{
            //    PageId = 1,
            //    PageVersionId = 1,
            //    Html = "<p>Here is some <strong>rich text</strong> for the home page.</p>"
            //});

            //context.RichTexts.Add(new RichText
            //{
            //    PageId = 1,
            //    PageVersionId = 2,
            //    Html = "<p>Ici est du <strong>riche texte</strong> pour la page d'acceuil!</p>"
            //});

            base.Seed(context);
        }

        private void CreateLanguages(WebsiteDbContext context)
        {
            var now = DateTimeOffset.Now;

            context.Languages.Add(new Language
            {
                Id = 1,
                LanguageVersions = new List<LanguageVersion>
                {
                    new LanguageVersion
                    {
                        Id = 1,
                        LanguageId = 1,
                        Language = 1,
                        Name = "English",
                        Dialect = "British"
                    },
                    new LanguageVersion
                    {
                        Id = 2,
                        LanguageId = 1,
                        Language = 2,
                        Name = "Anglais",
                        Dialect = "Britannique"
                    },
                    new LanguageVersion
                    {
                        Id = 3,
                        LanguageId = 1,
                        Language = 3,
                        Name = "English - German",
                        Dialect = "British"
                    },
                    new LanguageVersion
                    {
                        Id = 4,
                        LanguageId = 1,
                        Language = 4,
                        Name = "English - Spanish",
                        Dialect = "British"
                    }
                },
                LanguageCode = "en-gb",
                Created = now,
                Updated = now
            });

            context.Languages.Add(new Language
            {
                Id = 2,
                LanguageVersions = new List<LanguageVersion>
                {
                    new LanguageVersion
                    {
                        Id = 5,
                        LanguageId = 2,
                        Language = 1,
                        Name = "French"
                    }
                },
                LanguageCode = "fr-fr",
                Created = now,
                Updated = now
            });

            context.Languages.Add(new Language
            {
                Id = 3,
                LanguageVersions = new List<LanguageVersion>
                {
                    new LanguageVersion
                    {
                        Id = 7,
                        LanguageId = 3,
                        Language = 1,
                        Name = "German"
                    }
                },
                LanguageCode = "de-de",
                Created = now,
                Updated = now
            });

            context.Languages.Add(new Language
            {
                Id = 4,
                LanguageVersions = new List<LanguageVersion>
                {
                    new LanguageVersion
                    {
                        Id = 11,
                        LanguageId = 4,
                        Language = 1,
                        Name = "Spanish"
                    }
                },
                LanguageCode = "es-es",
                Created = now,
                Updated = now
            });
        }

        private void CreateArticles(WebsiteDbContext context)
        {
            var now = DateTimeOffset.Now;

            context.Articles.Add(new Article
            {
                ArticleVersions = new List<ArticleVersion>
                    {
                        new ArticleVersion
                        {
                            LanguageId = 1,
                            Title = "Lorem Ipsum",
                            Content = "<p>Lorem ipsum dolor sit amet, admodum sapientem ne duo. Ex quo mentitum similique, eu nec eirmod euismod. His possit salutandi id, ex nostrum recusabo reprimique pri. Cu pri modus percipit, vis ad moderatius scribentur.</p><p>Vim legimus civibus pertinacia te. Mei no minim clita constituam. Autem mandamus est no, vis posse integre salutandi ad, aliquip interpretaris pri ne. Saepe laoreet no per, mei eu error essent constituto. Partiendo periculis vel te, te melius sanctus vocibus mel. Ea inani utroque deserunt ius, his integre omittam percipitur ad.</p><p>Error animal eloquentiam ut eum. Vis ea duis liber, ut idque discere hendrerit est, adhuc decore atomorum his ex. An est eruditi accumsan dignissim, adhuc laudem appellantur pro no, at congue inermis usu. Sed ne iriure appellantur, mazim libris suscipiantur id nec.</p><p>Vidit nullam propriae pro cu, ipsum nusquam pro no, ex qui impetus sensibus. Mei in salutandi aliquando conclusionemque. Reque ullum viris ei nec, sed ut cibo insolens. Ad prima mandamus voluptatibus sed, per elit nihil principes eu, qui illud graeci aeterno no. Id vim probo consul.</p><p>Ad ius decore fabulas vituperata. Vel integre feugait at, vix an homero honestatis persequeris, luptatum hendrerit mel ei. Eu nec quidam adipiscing, pro an option saperet maiorum. Fabulas recteque an duo, mel ex laoreet conceptam contentiones, recteque qualisque id ius.</p><p>Augue simul fuisset ad est, nec in oportere molestiae, hinc pericula scribentur ne has. Mel nostrum gloriatur conceptam eu, mea in utinam aliquando appellantur. Mea ut quod omnesque, cetero habemus necessitatibus ei vis. Novum tantas sadipscing in vel, te velit incorrupte ius, at etiam numquam vim. Id pri solum dicunt persius, est dicam laudem delenit ut, pro ornatus temporibus an.</p><p>Ei usu possit denique, voluptua omittantur consectetuer ius eu, qui te putant offendit sententiae. Malorum moderatius eum te, et odio gloriatur liberavisse cum. Brute error deterruisset qui ad. Per no omnesque platonem tincidunt.</p><p>Ei sea alienum dissentias. Id eam bonorum partiendo instructior, eu augue clita qualisque sit. Option disputationi ei eum. Id eos graecis dolores, et sea justo dicat. Eos augue admodum singulis no, doctus commodo patrioque ei pro, ei pri tota fabellas tractatos.</p><p>Cu justo inermis comprehensam qui. Aeterno rationibus per eu. Purto viderer elaboraret ius cu. In modo vidisse pri, exerci dolorum pri ne, mea dico purto cu.</p><p>Percipit disputationi reprehendunt mea at. Mea ex nulla quando. Tacimates mandamus sed an, indoctum definiebas est ne, cum cu doming nemore. Assum disputationi vix ut, quo ad maluisset similique.</p>",
                            IsPublished = true,
                            Created = now,
                            Updated = now
                        }
                    },
                Published = now,
                Created = now,
                Updated = now
            });

            var article1 = new StringBuilder();
            article1.Append("<p>Eurotunnel says it expects to resume some vehicle and freight services through the Channel Tunnel on Saturday night following a lorry fire.</p>");
            article1.Append("<p>Eurostar, which operates passenger trains, cancelled 26 services on Saturday, but said it will run a \"full service\" on Sunday, with some delays.</p>");
            article1.Append("<p>But passengers without a train booking should not go to stations, it warned.</p>");
            article1.Append("<p>No-one was hurt in the fire, which occurred at the French end of the north tunnel.</p>");
            article1.Append("<p>Passengers were safely evacuated from trains in the tunnel, and the fire \"was quickly brought under control by the emergency services\", Eurotunnel said on its Twitter account.</p>");
            article1.Append("<p>The company, which runs vehicle and freight shuttles through the tunnel, said an inspection was under way and added that it anticipated resuming services later on Saturday evening through its south tunnel, which was unaffected by the fire.</p>");
            article1.Append("<h2>Further delays</h2>");
            article1.Append("<p>The director of public affairs at Eurotunnel, John Keefe, said the north tunnel was being cleaned up and having damage repaired.</p>");
            article1.Append("<p>Eurostar will be running a \"full service\" on Sunday for passengers who have an existing reservation for this date\", it said in a statement.</p>");
            article1.Append("<p>Services would be subject to delays of between 30 and 60 minutes because the north tunnel is expected to remain closed, it added.</p>");
            article1.Append("<p>\"We would strongly advise passengers whose journeys were impacted today by the problems in Eurotunnel not to come to our stations unless they have rebooked through our contact centre,\" Eurostar said.</p>");
            article1.Append("<p>Eurostar's customer care number is 03432 186 186, or +44 1777 777 878 for people outside the UK. </p>");
            article1.Append("<p>Earlier, Eurotunnel had said the alarm was raised when two CO2 detectors were triggered at 11:25 GMT.</p>");
            article1.Append("<p>A load on a lorry on board a train, en route from the UK to France, had been \"smouldering\", a Eurotunnel source said.</p>");
            article1.Append("<p>The train is still in the tunnel, about a third of the way from France.</p>");
            article1.Append("<p>Eurostar - which operates passenger services through the tunnel between Paris, London and Brussels - said 26 of its trains have been cancelled on Saturday afternoon.</p>");
            article1.Append("<p>One passenger Ben Lawton described how he was taken to a \"makeshift medical centre in Calais\" after being evacuated from a train inside the tunnel and given a gas mask.</p>");

            context.Articles.Add(new Article
            {
                ArticleVersions = new List<ArticleVersion>
                    {
                        new ArticleVersion
                        {
                            LanguageId = 1,
                            Title = "Eurotunnel expects to resume some services later",
                            Content = article1.ToString(),
                            IsPublished = true,
                            Created = now,
                            Updated = now
                        }
                    },
                Published = now,
                Created = now,
                Updated = now
            });
        }
    }
}