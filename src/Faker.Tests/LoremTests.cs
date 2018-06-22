using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class LoremTests {
      public LoremTests() {
         //var temp = "[abbas, abduco, abeo, abscido, absconditus, absens, absorbeo, absque, abstergo, absum, abundans, abutor, accedo, accendo, acceptus, accipio, accommodo, accusator, acer, acerbitas, acervus, acidus, acies, acquiro, acsi, adamo, adaugeo, addo, adduco, ademptio, adeo, adeptio, adfectus, adfero, adficio, adflicto, adhaero, adhuc, adicio, adimpleo, adinventitias, adipiscor, adiuvo, administratio, admiratio, admitto, admoneo, admoveo, adnuo, adopto, adsidue, adstringo, adsuesco, adsum, adulatio, adulescens, adultus, aduro, advenio, adversus, advoco, aedificium, aeger, aegre, aegrotatio, aegrus, aeneus, aequitas, aequus, aer, aestas, aestivus, aestus, aetas, aeternus, ager, aggero, aggredior, agnitio, agnosco, ago, ait, aiunt, alienus, alii, alioqui, aliqua, alius, allatus, alo, alter, altus, alveus, amaritudo, ambitus, ambulo, amicitia, amiculum, amissio, amita, amitto, amo, amor, amoveo, amplexus, amplitudo, amplus, ancilla, angelus, angulus, angustus, animadverto, animi, animus, annus, anser, ante, antea, antepono, antiquus, aperio, aperte, apostolus, apparatus, appello, appono, appositus, approbo, apto, aptus, apud, aqua, ara, aranea, arbitro, arbor, arbustum, arca, arceo, arcesso, arcus, argentum, argumentum, arguo, arma, armarium, armo, aro, ars, articulus, artificiose, arto, arx, ascisco, ascit, asper, aspicio, asporto, assentator, astrum, atavus, ater, atqui, atrocitas, atrox, attero, attollo, attonbitus, auctor, auctus, audacia, audax, audentia, audeo, audio, auditor, aufero, aureus, auris, aurum, aut, autem, autus, auxilium, avaritia, avarus, aveho, averto, avoco, baiulus, balbus, barba, bardus, basium, beatus, bellicus, bellum, bene, beneficium, benevolentia, benigne, bestia, bibo, bis, blandior, bonus, bos, brevis, cado, caecus, caelestis, caelum, calamitas, calcar, calco, calculus, callide, campana, candidus, canis, canonicus, canto, capillus, capio, capitulus, capto, caput, carbo, carcer, careo, caries, cariosus, caritas, carmen, carpo, carus, casso, caste, casus, catena, caterva, cattus, cauda, causa, caute, caveo, cavus, cedo, celebrer, celer, celo, cena, cenaculum, ceno, censura, centum, cerno, cernuus, certe, certo, certus, cervus, cetera, charisma, chirographum, cibo, cibus, cicuta, cilicium, cimentarius, ciminatio, cinis, circumvenio, cito, civis, civitas, clam, clamo, claro, clarus, claudeo, claustrum, clementia, clibanus, coadunatio, coaegresco, coepi, coerceo, cogito, cognatus, cognomen, cogo, cohaero, cohibeo, cohors, colligo, colloco, collum, colo, color, coma, combibo, comburo, comedo, comes, cometes, comis, comitatus, commemoro, comminor, commodo, communis, comparo, compello, complectus, compono, comprehendo, comptus, conatus, concedo, concido, conculco, condico, conduco, confero, confido, conforto, confugo, congregatio, conicio, coniecto, conitor, coniuratio, conor, conqueror, conscendo, conservo, considero, conspergo, constans, consuasor, contabesco, contego, contigo, contra, conturbo, conventus, convoco, copia, copiose, cornu, corona, corpus, correptius, corrigo, corroboro, corrumpo, coruscus, cotidie, crapula, cras, crastinus, creator, creber, crebro, credo, creo, creptio, crepusculum, cresco, creta, cribro, crinis, cruciamentum, crudelis, cruentus, crur, crustulum, crux, cubicularis, cubitum, cubo, cui, cuius, culpa, culpo, cultellus, cultura, cum, cunabula, cunae, cunctatio, cupiditas, cupio, cuppedia, cupressus, cur, cura, curatio, curia, curiositas, curis, curo, curriculum, currus, cursim, curso, cursus, curto, curtus, curvo, curvus, custodia, damnatio, damno, dapifer, debeo, debilito, decens, decerno, decet, decimus, decipio, decor, decretum, decumbo, dedecor, dedico, deduco, defaeco, defendo, defero, defessus, defetiscor, deficio, defigo, defleo, defluo, defungo, degenero, degero, degusto, deinde, delectatio, delego, deleo, delibero, delicate, delinquo, deludo, demens, demergo, demitto, demo, demonstro, demoror, demulceo, demum, denego, denique, dens, denuncio, denuo, deorsum, depereo, depono, depopulo, deporto, depraedor, deprecator, deprimo, depromo, depulso, deputo, derelinquo, derideo, deripio, desidero, desino, desipio, desolo, desparatus, despecto, despirmatio, infit, inflammatio,  paens, patior, patria, patrocinor, patruus, pauci, paulatim, pauper, pax, peccatus, pecco, pecto, pectus, pecunia, pecus, peior, pel, ocer, socius, sodalitas, sol, soleo, solio, solitudo, solium, sollers, sollicito, solum, solus, solutio, solvo, somniculosus, somnus, sonitus, sono, sophismata, sopor, sordeo, sortitus, spargo, speciosus, spectaculum, speculum, sperno, spero, spes, spiculum, spiritus, spoliatio, sponte, stabilis, statim, statua, stella, stillicidium, stipes, stips, sto, strenuus, strues, studio, stultus, suadeo, suasoria, sub, subito, subiungo, sublime, subnecto, subseco, substantia, subvenio, succedo, succurro, sufficio, suffoco, suffragium, suggero, sui, sulum, sum, summa, summisse, summopere, sumo, sumptus, supellex, super, suppellex, supplanto, suppono, supra, surculus, surgo, sursum, suscipio, suspendo, sustineo, suus, synagoga, tabella, tabernus, tabesco, tabgo, tabula, taceo, tactus, taedium, talio, talis, talus, tam, tamdiu, tamen, tametsi, tamisium, tamquam, tandem, tantillus, tantum, tardus, tego, temeritas, temperantia, templum, temptatio, tempus, tenax, tendo, teneo, tener, tenuis, tenus, tepesco, tepidus, ter, terebro, teres, terga, tergeo, tergiversatio, tergo, tergum, termes, terminatio, tero, terra, terreo, territo, terror, tersus, tertius, testimonium, texo, textilis, textor, textus, thalassinus, theatrum, theca, thema, theologus, thermae, thesaurus, thesis, thorax, thymbra, thymum, tibi, timidus, timor, titulus, tolero, tollo, tondeo, tonsor, torqueo, torrens, tot, totidem, toties, totus, tracto, trado, traho, trans, tredecim, tremo, trepide, tres, tribuo, tricesimus, triduana, triginta, tripudio, tristis, triumphus, trucido, truculenter, tubineus, tui, tum, tumultus, tunc, turba, turbo, turpe, turpis, tutamen, tutis, tyrannus, uberrime, ubi, ulciscor, ullus, ulterius, ultio, ultra, umbra, umerus, umquam, una, unde, undique, universe, unus, urbanus, urbs, uredo, usitas, usque, ustilo, ustulo, usus, uter, uterque, utilis, utique, utor, utpote, utrimque, utroque, utrum, uxor, vaco, vacuus, vado, vae, valde, valens, valeo, valetudo, validus, vallum, vapulus, varietas, varius, vehemens, vel, velociter, velum, velut, venia, venio, ventito, ventosus, ventus, venustas, ver, verbera, verbum, vere, verecundia, vereor, vergo, veritas, vero, versus, verto, verumtamen, verus, vesco, vesica, vesper, vespillo, vester, vestigium, vestrum, vetus, via, vicinus, vicissitudo, victoria, victus, videlicet, video, viduata, viduo, vigilo, vigor, vilicus, vilis, vilitas, villa, vinco, vinculum, vindico, vinitor, vinum, vir, virga, virgo, viridis, viriliter, virtus, vis, viscus, vita, vitiosus, vitium, vito, vivo, vix, vobis, vociferor, voco, volaticus, volo, volubilis, voluntarius, volup, volutabrum, volva, vomer, vomica, vomito, vorago, vorax, voro, vos, votum, voveo, vox, vulariter, vulgaris, vulgivagus, vulgo, vulgus, vulnero, vulnus, vulpes, vulticulus, vultuosus, xiphias]";
         //_supplementalWords = temp.Trim('[', ']').Split(',').Select(x => x.Trim()).ToArray();
      }

      [SetUp]
      public void SetUp() {
         RandomNumber.ResetSeed(42);
      }

      [Test]
      public void Character_HappyDays() {
         // arrange

         // act
         var character = Lorem.Character();

         // assert
         Assert.AreEqual(1, character.Length);
      }

      [Test]
      public void Characters_With_Default_Value() {
         // arrange

         // act
         var characters = Lorem.Characters();

         // assert
         Assert.AreEqual(255, characters.Length);
         Assert.AreEqual(0, characters.Count(c => c == ' '));
      }

      [Test]
      public void Characters_With_Random_CharCount() {
         // arrange
         var charCount = RandomNumber.Next(42, 84);

         // act
         var characters = Lorem.Characters(charCount);

         // assert
         Assert.AreEqual(charCount, characters.Length);
         Assert.AreEqual(0, characters.Count(c => c == ' '));
      }

      [Test]
      public void Characters_With_Invalid_CharCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Characters(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: charCount", ex.Message);
      }

      [Test]
      public void Characters_Returns_Empty_String_When_CharCount_Zero() {
         // arrange

         // act
         var characters = Lorem.Characters(0);

         // assert
         Assert.AreEqual(string.Empty, characters);
      }

      [Test]
      public void Word_HappyDays() {
         // arrange

         // act
         var word = Lorem.Word();

         // assert
         Assert.AreEqual("libero", word);
      }

      [Test]
      public void Word_Twice_NotEqual() {
         // arrange

         // act
         var word1 = Lorem.Word();
         var word2 = Lorem.Word();

         // assert
         Assert.AreNotEqual(word1, word2);
      }

      [Test]
      public void Words_HappyDays() {
         // arrange

         // act
         var words = Lorem.Words();

         // assert
         Assert.AreEqual(3, words.Count());
         Assert.AreEqual("cupiditate dolorem voluptatem", string.Join(" ", words));
         Assert.AreEqual(0, words.Count(x => string.IsNullOrWhiteSpace(x)));
      }

      [Test]
      public void Words_With_WordCount() {
         // arrange
         const int wordCount = 42;

         // act
         var words = Lorem.Words(wordCount);

         // assert
         Assert.AreEqual(wordCount, words.Count());
         Assert.AreEqual(0, words.Count(x => string.IsNullOrWhiteSpace(x)));
      }

      [Test]
      public void Words_With_Invalid_WordCount() {
         // arrange
         var invalidWordCount = -1;

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Words(invalidWordCount));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Words_With_WordCount_Zero_Returns_Empty_Enumerable() {
         // arrange

         // act
         var words = Lorem.Words(0);

         // assert
         Assert.AreEqual(0, words.Count());
      }

      [Test]
      public void Words_With_Supplemental() {
         // arrange
         const int defaultWordCount = 3;

         // act
         var words = Lorem.Words(supplemental: true);

         // assert
         Assert.AreEqual(defaultWordCount, words.Count());
         Assert.AreEqual("deficio candidus vel", string.Join(" ", words));
      }

      [Test]
      public void Words_Twice_Are_Different() {
         // arrange

         // act
         var words1 = Lorem.Words();
         var words2 = Lorem.Words();

         // assert
         Assert.AreNotEqual(words1, words2);
      }

      [Test]
      public void Words_WithSupplementalWords_Twice_Are_Different() {
         // arrange

         // act
         var words1 = Lorem.Words(supplemental: true);
         var words2 = Lorem.Words(supplemental: true);

         // assert
         Assert.AreNotEqual(words1, words2);
      }

      [Test]
      public void Multibyte_HappyDays() {
         // arrange

         // act
         var multibyte = Lorem.Multibyte();

         // assert
         Assert.AreEqual(1, multibyte.Length);
         var withoutNonAscii = Regex.Replace(multibyte, @"[^\u0000-\u007F]", string.Empty);
         Assert.AreEqual(0, withoutNonAscii.Length);
      }

      [Test]
      public void Sentence_HappyDays() {
         // arrange

         // act
         var sentence = Lorem.Sentence(/* 4 is default */);

         // assert
         Assert.IsTrue(sentence.EndsWith("."));
         Assert.AreEqual(0, Regex.Match(sentence, "^[A-Z]").Index); // first letter to be upper case
         var matches = Regex.Matches(sentence, "[a-z]+");
         Assert.GreaterOrEqual(matches.Count, 4);
         Assert.GreaterOrEqual(sentence.Count(c => c == ' '), 3);
      }

      [Test]
      public void Sentence_Non_Default_Value() {
         // arrange
         const int minimumWordCount = 42;

         // act
         var sentence = Lorem.Sentence(minimumWordCount);

         // assert
         var matches = Regex.Matches(sentence, "[a-zA-Z]+");
         Assert.GreaterOrEqual(matches.Count, minimumWordCount);
      }

      [Test]
      public void Sentence_With_Exact_WordCount() {
         // arrange
         var randomWordCount = RandomNumber.Next(42);

         // act
         var sentence = Lorem.Sentence(randomWordCount, randomWordsToAdd: 0);

         // assert
         var matches = Regex.Matches(sentence, "[a-zA-Z]+");
         Assert.AreEqual(randomWordCount, matches.Count);
      }

      [Test]
      public void Sentence_With_Words_From_Supplementary_List() {
         // arrange
         RandomNumber.ResetSeed(42);
         var wordCount = 7;

         // act
         var sentence = Lorem.Sentence(wordCount, supplemental: true).ToLower();

         // assert
         Assert.IsTrue(_supplementalWords.Any(x => sentence.Contains(x)));
      }

      [Test]
      public void Sentence_With_Invalid_WordCount() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentence(-1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: wordCount", ex.Message);
      }

      [Test]
      public void Sentence_With_Invalid_RandomWordsToAdd() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Lorem.Sentence(7, randomWordsToAdd: -1));

         // assert
         Assert.AreEqual("Must be equal to or greater than zero.\r\nParameter name: randomWordsToAdd", ex.Message);
      }

      [Test]
      public void Sentence_With_Exact_WordCount_Of_Zero_Is_Empty_String() {
         // arrange

         // act
         var sentence = Lorem.Sentence(0, randomWordsToAdd: 0);

         // assert
         Assert.AreEqual(string.Empty, sentence);
      }

      [Test]
      public void Sentences_HappyDays() {
         // arrange

         // act
         var sentences = Lorem.Sentences(7);

         // assert
         Assert.AreEqual(7, sentences.Count());
         Assert.AreEqual(7, sentences.Count(x => x.EndsWith(".")));
         Assert.AreEqual(7, sentences.Count(x => x.Contains(" ")));
      }

      [Test]
      public void Paragraph_HappyDays() {
         // arrange

         // act
         var paragraph = Lorem.Paragraph(/* 3 is default */);

         // assert
         Assert.GreaterOrEqual(paragraph.Count(c => c == '.'), 3);
         Assert.Greater(paragraph.Count(c => c == ' '), 9);
      }

      [Test]
      public void Paragraphs_HappyDays() {
         // arrange

         // act
         var paragraphs = Lorem.Paragraphs(7);

         // assert
         Assert.AreEqual(7, paragraphs.Count());
         Assert.AreEqual(7, paragraphs.Count(x => x.EndsWith(".")));
         Assert.AreEqual(7, paragraphs.Count(x => x.Contains(" ")));
      }

      private string[] _supplementalWords => Lorem.SupplementalWords;
   }
}
