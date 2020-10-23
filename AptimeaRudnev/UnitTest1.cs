using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Threading;

namespace AptimeaRudnev
{
    public class Tests
    {
        String test_url = "http://prelive.aptimea.com";
        static Random rnd = new Random();
        IWebDriver driver;

        [SetUp]
        public void start_Browser()
        {
            // Local Selenium WebDriver
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void test_login()
        {
            driver.Url = test_url;
            Thread.Sleep(200);
            driver.Navigate().GoToUrl("http://prelive.aptimea.com/user/login");
            driver.FindElement(By.Id("edit-name")).SendKeys("klassivend870@gmail.com");
            driver.FindElement(By.Id("edit-pass")).SendKeys("123123");
            driver.FindElement(By.Id("edit-submit")).Click();
            Thread.Sleep(2000);
            if (driver.FindElements(By.CssSelector(".alert-danger")).Count != 0)
            {
                if (driver.FindElement(By.Id("edit-pass")).GetAttribute("value").Length==0)
                {
                    driver.FindElement(By.Id("edit-pass")).SendKeys("123123");
                }
                driver.FindElement(By.Id("edit-submit")).Click();
                while (driver.FindElements(By.CssSelector(".alert-danger")).Count != 0)
                {
                    if (driver.FindElement(By.Id("edit-pass")).GetAttribute("value").Length == 0)
                    {
                        driver.FindElement(By.Id("edit-pass")).SendKeys("123123");
                    }
                    driver.FindElement(By.Id("edit-submit")).Click();
                }
            }

        }
        [Test]
        public void test_analys()
        {
            driver.Url = test_url;

            Thread.Sleep(200);
            driver.Navigate().GoToUrl("http://prelive.aptimea.com/form/questionnaire");


            IWebElement cookieButton = driver.FindElement(By.CssSelector(".eu-cookie-compliance-secondary-button"));
            cookieButton.Click();
            Thread.Sleep(2000);
            void clickRandomAllNumeric(string value)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> list = driver.FindElements(By.XPath("//input[@type='number']"));
                    foreach (var item2 in list)
                    {
                        item2.SendKeys(value);
                    }
                }
                catch (Exception) {  }
            }
            List<string> selectors = new List<string>();
            void randomELChange(string selector)
            {
                ReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector($"{selector}"));
                int r = rnd.Next(list.Count);
                list[r].Click();
            }
            void clickRandomAllRadio(string[] els)
            {
                selectors.AddRange(els);
                selectors.ForEach(s =>
                {
                    randomELChange($"[name= '{s}']");
                });
                selectors.Clear();
            }
            IWebElement yearSelect = driver.FindElement(By.Id("edit-je-suis-ne-e-en-annee-year"));
            SelectElement selectElement = new SelectElement(yearSelect);
            selectElement.SelectByValue("2001");
            try
            {
                clickRandomAllRadio(new string[] { "je_suis", "je_suis_2", "je_vis", "je_fais_du_sport_chaque_semaine", "patient_goals" });
            }
            catch (Exception){}
            Thread.Sleep(2500);
            if (driver.FindElement(By.CssSelector("#edit-je-suis-enceinte--wrapper")).Displayed)
            {
                clickRandomAllRadio(new string[] { "je_suis_enceinte" });
            }
            IWebElement supplementsText = driver.FindElement(By.CssSelector("[name = 'mes_traitements_medicaux_sont']"));
            supplementsText.SendKeys("Vitamin A, Vitamin B");
            clickRandomAllNumeric("1");
            

            driver.FindElement(By.XPath("//button[@data-drupal-selector='edit-wizard-next']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//button[@data-drupal-selector='edit-wizard-next']")).Click();
            while (driver.FindElements(By.CssSelector(".alert-danger")).Count != 0)
            {
                driver.FindElement(By.XPath("//button[@data-drupal-selector='edit-wizard-next']")).Click();
            }
            Thread.Sleep(5000);

            void nextPage(string[] collections, bool editedpage5 = false)
            {
                clickRandomAllRadio(collections);
                /*  if (editedpage5)
                  {
                       clickRandomAllRadio(new string[] { "je_prends_une_contraception_orale_ou_un_traitement_hormonal" });

                       if (driver.FindElements(By.CssSelector("[name='je_prends_une_contraception_orale_ou_un_traitement_hormonal']")).Count != 0)
                       {
                           clickRandomAllRadio(new string[] { "je_prends_une_contraception_orale_ou_un_traitement_hormonal" });
                       }
            }*/
                driver.FindElement(By.XPath("//button[@data-drupal-selector='edit-wizard-next']")).Click();
                Thread.Sleep(5000);
            }
            try
            {
                nextPage(new string[] { "fatigue_surtout_matinale_difficultes_a_demarrer", "j_ai_des_reveils_nocturnes_frequents_ou_precoces", "difficulte_d_endormissement",
              "j_ai_l_impression_de_ne_pas_dormir_profondement_d_avoir_un_somme", "je_suis_souvent_somnolent_fatigue_apres_les_repas", "je_suis_reveille_vers_3_4h_du_matin",
                  "je_me_couche_tard_me_leve_tot_regarde_des_ecrans_le_soir_je_dors", "je_me_sens_epuise_meme_apres_le_we_ou_les_vacances", "fatigue_depuis_plus_de_3mois",
              "j_ai_des_taches_cutanees_qui_sont_apparues_avec_l_age_j_ai_des_r"});
                nextPage(new string[] { "grignotage_fringale_compulsion_addiction_tabac_sport_alimentaire", "j_ai_la_sensation_de_faire_des_malaises_hypoglycemiques",
            "envie_de_sucre_en_fin_de_journee", "j_ai_mal_a_la_tete_des_migraines_en_fonction_de_l_alimentation_s", "je_fume" });

                nextPage(new string[] { "je_ne_reconnais_pas_la_satiete", "je_mange_sans_faim_de_maniere_systematique_parce_que_c_est_l_heu", "je_suis_souvent_en_restriction_alimentaire",
             "je_me_ressers_j_ai_tendance_a_beaucoup_manger", "je_mange_quand_je_suis_fatiguee", "je_mange_quand_je_suis_stressee", "je_prends_facilement_un_verre_d_alcool_le_soir",
            "j_ai_de_la_cellulite_sur_les_jambes_les_bras", "j_ai_des_graisses_localisees_au_niveau_abdominale", "je_perds_du_poids_difficilement", "je_prends_du_poids_a_l_arret_de_l_activite_physique",
            "je_n_arrive_pas_a_prendre_du_poids"});

                nextPage(new string[] { "traitement_contre_les_douleurs_des_anti_inflammatoires_antalgiqu", "traitement_contre_l_acidite_de_l_estomac", 
                    "traitement_contre_les_troubles_du_transit_constipation_ou_diarrh","traitement_pour_dormir", "je_prends_un_traitement_contre_l_anxiete_l_angoisse",
                    "je_prends_un_traitement_anti_depresseur", "traitement_contre_l_allergie_l_asthme_permanent_ou_saisonnier_",
            "j_ai_pris_des_antibiotiques_dans_ma_premiere_annee_de_vie", /*"je_prends_une_contraception_orale_ou_un_traitement_hormonal",*/
                    "je_prends_un_ttt_contre_ou_j_ai_un_diabete_une_hta_du_cholestero"});
                if (driver.FindElements(By.CssSelector(".alert-danger")).Count != 0)
                {
                    Thread.Sleep(5000);
                    driver.FindElement(By.XPath("//button[@data-drupal-selector='edit-wizard-next']")).Click();
                    Thread.Sleep(2500);
                    while (driver.FindElements(By.CssSelector(".alert-danger")).Count != 0)
                    {
                        clickRandomAllRadio(new string[] { "je_prends_une_contraception_orale_ou_un_traitement_hormonal" });
                        Thread.Sleep(2500);
                        driver.FindElement(By.XPath("//button[@data-drupal-selector='edit-wizard-next']")).Click();
                    }
                    Thread.Sleep(5000);
                }

                nextPage(new string[] { "j_ai_des_atcd_personnels_de_diabete_gestationnel", "j_ai_fait_un_infarctus_un_avc", "j_ai_fait_des_calculs_renaux_de_la_goutte", "j_ai_une_maladie_autoimmune_diabete_de_type_1_polyarthrite_rhuma",
                "j_ai_une_maladie_rhumatismale", "j_ai_une_maladie_intestinale_crohn_rch_ou_je_prends_un_traitemen", "j_ai_une_maladie_coeliaque", "je_fais_de_l_arthrose",
                "j_ai_de_l_osteoporose_osteopenie", "j_ai_des_allergies_rhinite_conjonctivite_de_l_asthme_de_l_urtica", "j_ai_ete_opere_de_la_cataracte" });
                nextPage(new string[] { "j_ai_des_douleurs_musculaires_sur_des_zones_differentes", "j_ai_des_douleurs_articulaires_qui_changent_de_localisation_",
                "j_ai_des_douleurs_articulaires_fixes_dos_cervicales_genou_hanche", "j_ai_des_crampes_la_paupiere_ou_les_muscles_qui_tressautent", "j_ai_j_ai_eu_un_cancer", "j_ai_eu_une_mni_un_zona",
                "je_suis_devenu_frileux", "j_ai_fait_une_infection_a_helicobacter_pylori", "j_ai_ete_opere_de_l_appendicite", "j_ai_fait_une_hepatite", "j_ai_de_la_parodontite",
                "je_fais_de_la_tetanie_ou_de_la_spasmophilie", "je_suis_devenu_allergique_avec_l_age" });
                nextPage(new string[] { "je_fais_des_oedemes_de_la_retention_d_eau", "j_ai_une_langue_blanche_ou_verte_chargee", "j_ai_des_aphtes_des_sensations_de_brulures_buccales",
                    "syndrome_des_jambes_sans_repos_impatiences_dans_les_jambes", "j_ai_des_remontees_acides_oesophagiennes_j_ai_un_reflux_gastrooe",
                    "j_ai_des_varices_des_varicosites_de_l_insuffisance_veineuse_des_", "je_perds_mes_cheveux_mes_ongles_sont_cassants_",
                    "j_ai_la_peau_seche_les_yeux_secs", "j_ai_de_l_acne_rosacee", "je_fais_souvent_des_regimes", "je_fais_des_tendinites_des_dechirures_musculaires_des_entorses",
                    "j_ai_des_infections_orl_pulmonaire_bronchique", "je_fais_de_l_herpes_",
                    "j_ai_des_infections_urinaires_gynecologiques_prostatique_une_pye", "j_ai_eu_des_mycoses_cutanes_gynecologiques_du_muguet_buccal",
                    "je_fais_des_gastroenterites", "j_ai_un_syndrome_premenstruel_je_gonfle_avant_les_regles_j_ai_ma", "je_fais_des_crises_d_angoisses_", "je_prends_des_antibiotiques" });
                nextPage(new string[] { "certains_aliments_me_donnent_de_troubles_digestifs", "j_ai_de_la_constipation_ou_un_transit_irregulier", "j_ai_de_la_diarrhee_ou_alterne_diarrhee_constipation",
                    "j_ai_des_selles_molles_collantes", "j_ai_des_demangeaisons_des_brulures_anales", "j_ai_des_nausees_je_suis_facilement_ecoeure_envie_de_vomir_facil",
                    "j_ai_une_digestion_lente_sensation_d_estomac_plein_longtemps_apr", "j_ai_des_douleurs_des_spasmes_abdominaux", "je_suis_ballonne_j_ai_des_gaz_des_rots" });
                nextPage(new string[] { "trouble_de_memoire_concentration_attention", "j_ai_tendance_au_repli_sur_moi_je_sors_moins_", "_moins_d_envie_de_desir_tendance_deprimee",
                    "moins_de_plaisir_a_faire_les_activites_habituelles", "j_ai_moins_de_motivation_dans_les_activites_habituelles_y_compri", "intolerance_a_la_frustration_impatient",
                    "_je_suis_plus_irritable_agressif", "vulnerabilite_au_stress_anxiete_je_suis_facilement_deborde_anxie", "tendance_a_ruminer_j_ai_le_cerveau_qui_ne_s_arrete_jamais",
                    "j_ai_l_impression_de_fonctionner_au_ralenti_d_avoir_des_difficul", "j_ai_les_mains_moites_je_transpire_facilement", "j_ai_des_palpitations_cardiaques_des_troubles_du_rythme_de_la_ta" });
                
                
                driver.FindElement(By.CssSelector("[data-drupal-selector='edit-submit']")).Click();
                driver.FindElement(By.CssSelector("[href='/user/login']")).Click();
                driver.FindElement(By.Id("edit-name")).SendKeys("klassivend870@gmail.com");
                driver.FindElement(By.Id("edit-pass")).SendKeys("123123");
                Thread.Sleep(2000);
                driver.FindElement(By.Id("edit-submit")).Click();
                if (driver.FindElements(By.CssSelector(".alert-danger")).Count != 0)
                {
                    if (driver.FindElement(By.Id("edit-pass")).GetAttribute("value").Length == 0)
                    {
                        driver.FindElement(By.Id("edit-pass")).SendKeys("123123");
                    }
                    driver.FindElement(By.Id("edit-submit")).Click();
                    while (driver.FindElements(By.CssSelector(".alert-danger")).Count != 0)
                    {
                        if (driver.FindElement(By.Id("edit-pass")).GetAttribute("value").Length == 0)
                        {
                            driver.FindElement(By.Id("edit-pass")).SendKeys("123123");
                        }
                        driver.FindElement(By.Id("edit-submit")).Click();
                    }
                }
            }
            catch (Exception)
            {
                Thread.Sleep(10000);
                throw;
            }
            
            Thread.Sleep(10000);
            Console.WriteLine("Test Passed");
        }

        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }
}


/* try
 {
     ReadOnlyCollection<IWebElement> list = driver.FindElements(By.XPath("//input[@type='radio']"));
     Thread.Sleep(200);
     int r = rnd.Next(list.Count);
     list[r].Click();
     foreach (var item2 in list)
     {
         item2.Click();
     }
 }
 catch (Exception)
 {
     throw;
 }

 IWebElement nextPageButton2 = driver.FindElement(By.Id("edit-wizard-next--oVRg9_hscOE"));
 Thread.Sleep(2000);
 nextPageButton2.Click();*/
/*randomELChange("[name='fatigue_surtout_matinale_difficultes_a_demarrer']");
randomELChange("[name='j_ai_des_reveils_nocturnes_frequents_ou_precoces']");*/
//List<string> selectors = new List<string>() { "fatigue_surtout_matinale_difficultes_a_demarrer", "j_ai_des_reveils_nocturnes_frequents_ou_precoces" };
/*selectors.ForEach(s =>
{
    randomELChange($"[name='{s}']");
});*/
/*var list2 = driver.FindElements(By.CssSelector("[name='patient_goals']"));
foreach (var item2 in list2)
{
    System.Threading.Thread.Sleep(200);
    item2.Click();
}*/


/*Actions actions = new Actions(driver);
actions.MoveToElement(searchButton);
actions.Perform();*/
/*IWebElement weightNumeric = driver.FindElement(By.Id("edit-user-weight"));
weightNumeric.SendKeys("70");
IWebElement heightNumeric = driver.FindElement(By.Id("edit-user-height"));
heightNumeric.SendKeys("182");
IWebElement kidsNumeric = driver.FindElement(By.CssSelector("#edit-j-ai-enfants-nombre-"));
kidsNumeric.SendKeys("1");*/
/*   try
   {
       ReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector("textarea"));
       foreach (var item2 in list)
       {
           item2.SendKeys("1");
       }
   }
   catch (Exception) {throw; }*/
/*IWebElement genderRadio = driver.FindElement(By.Id("edit-je-suis-0"));
genderRadio.Click();
IWebElement yearSelect = driver.FindElement(By.Id("edit-je-suis-ne-e-en-annee-year"));
SelectElement selectElement = new SelectElement(yearSelect);
selectElement.SelectByValue("2001");
IWebElement sportRadio = driver.FindElement(By.Id("edit-je-fais-du-sport-chaque-semaine-0"));
sportRadio.Click();

IWebElement supplementsText = driver.FindElement(By.CssSelector("[name = 'mes_traitements_medicaux_sont']"));
supplementsText.SendKeys("Vitamin A, Vitamin B");
IWebElement weightNumeric = driver.FindElement(By.Id("edit-user-weight"));
weightNumeric.SendKeys("70");
IWebElement heightNumeric = driver.FindElement(By.Id("edit-user-height"));
heightNumeric.SendKeys("182");
IWebElement kidsNumeric = driver.FindElement(By.CssSelector("#edit-j-ai-enfants-nombre-"));
kidsNumeric.SendKeys("1");

IWebElement veganRadio = driver.FindElement(By.Id("edit-je-suis-2-2"));
veganRadio.Click();
IWebElement positionRadio = driver.FindElement(By.Id("edit-je-vis-0"));
positionRadio.Click();
IWebElement pointGoalRadio = driver.FindElement(By.CssSelector("#edit-patient-goals-43"));
pointGoalRadio.Click();*/

/*ReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector("[name='je_suis']"));
foreach (var item in list)
{
    System.Threading.Thread.Sleep(200);
    item.Click();
}*/