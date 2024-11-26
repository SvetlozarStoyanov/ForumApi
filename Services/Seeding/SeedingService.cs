using Contracts.Services.Seeding;
using Database;
using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Entities.Seeding;
using Database.Entities.Subforums;
using Database.Entities.Votes;
using Database.Enums.Votes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;

namespace Services.Seeding
{
    public class SeedingService : ISeedingService
    {
        private readonly ForumDbContext context;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        private static ApplicationUser[] users =
        {
            new ApplicationUser() { Id = "6dc20f7b-7ec4-4c95-9a83-673d4dcf28a6", UserName = "KevinPrice",  Email = "kevinprice@mail.com", PhoneNumber = "6538136373" },
            new ApplicationUser() { Id = "6253fcfb-4f3b-49f9-b3aa-ee9cb4209fb7", UserName = "NicoleMurray",  Email = "nicolemurray@mail.com", PhoneNumber = "8043492443" },
            new ApplicationUser() { Id = "bb372c8a-964c-4611-b397-484a130a2008", UserName = "LucasWarren",  Email = "lucaswarren@mail.com", PhoneNumber = "6360076621" },
            new ApplicationUser() { Id = "4215dfc6-4093-4037-86d8-d7ef5233acec", UserName = "DavidAnderson",  Email = "davidanderson@mail.com", PhoneNumber = "9232798073" },
            new ApplicationUser() { Id = "9e35c0bb-b498-4b9f-8cc3-77853a84a743", UserName = "NathanFisher",  Email = "nathanfisher@mail.com", PhoneNumber = "9105855435" },
            new ApplicationUser() { Id = "ef28d9d2-fe39-46a0-8e2e-e8068c4d63ca", UserName = "HelenMurphy",  Email = "helenmurphy@mail.com", PhoneNumber = "5652313783" },
            new ApplicationUser() { Id = "c0268b88-b66d-4965-ae0b-191b25fdf4b2", UserName = "MarkPhillips",  Email = "markphillips@mail.com", PhoneNumber = "9770606208" },
            new ApplicationUser() { Id = "3044c240-a032-4972-82b5-61cdd68010bb", UserName = "BarbaraClark",  Email = "barbaraclark@mail.com", PhoneNumber = "8138359039" },
            new ApplicationUser() { Id = "cf9712bc-8504-4ae6-b37a-bef26552a6ab", UserName = "JasonHoward",  Email = "jasonhoward@mail.com", PhoneNumber = "6352528940" },
            new ApplicationUser() { Id = "b86653ed-cfee-4384-bea4-e3e211f2d6dd", UserName = "SophiaRichards",  Email = "sophiarichards@mail.com", PhoneNumber = "5655805111" },
            new ApplicationUser() { Id = "a797c836-f2f4-4bbe-b9ec-dd8190c3f3ba", UserName = "RobertHarris",  Email = "robertharris@mail.com", PhoneNumber = "9899281847" },
            new ApplicationUser() { Id = "f2243382-d292-48a8-9754-537f9a36153e", UserName = "AnthonyBrooks",  Email = "anthonybrooks@mail.com", PhoneNumber = "4852179909" },
            new ApplicationUser() { Id = "efa5d1d6-ae8c-4b0f-817d-cb695d42c774", UserName = "CharlesHall",  Email = "charleshall@mail.com", PhoneNumber = "1775398472" },
            new ApplicationUser() { Id = "db197782-54e7-4417-b5d2-7a2acd20ce7a", UserName = "BrendaCox",  Email = "brendacox@mail.com", PhoneNumber = "7464712136" },
            new ApplicationUser() { Id = "3f651191-9b8c-4e93-9396-b3d90ec2963e", UserName = "ZacharyCruz",  Email = "zacharycruz@mail.com", PhoneNumber = "7697351104" },
            new ApplicationUser() { Id = "2a1f527c-bf15-4d94-ac3b-2d9f3222ef8d", UserName = "EmilyDavis",  Email = "emilydavis@mail.com", PhoneNumber = "7976291014" },
            new ApplicationUser() { Id = "9d67b356-f6b4-4860-b8d2-c868f692a50b", UserName = "RebeccaWard",  Email = "rebeccaward@mail.com", PhoneNumber = "9851937964" },
            new ApplicationUser() { Id = "fca83477-1982-475b-873e-586cb784554c", UserName = "JamesMoore",  Email = "jamesmoore@mail.com", PhoneNumber = "3078290940" },
            new ApplicationUser() { Id = "7290eba5-bb34-44d5-9203-4970b1b94f21", UserName = "AndrewCooper",  Email = "andrewcooper@mail.com", PhoneNumber = "7774767198" },
            new ApplicationUser() { Id = "d309c553-1964-44e2-aeed-241868ec7db5", UserName = "JosephRobinson",  Email = "josephrobinson@mail.com", PhoneNumber = "8498491859" },
            new ApplicationUser() { Id = "2516799d-1b9b-48aa-9178-4696b2988d8b", UserName = "AaronWest",  Email = "aaronwest@mail.com", PhoneNumber = "5164006623" },
            new ApplicationUser() { Id = "e059fe0c-0ddc-43f7-af4c-d68b1f1f790a", UserName = "JoshuaMorris",  Email = "joshuamorris@mail.com", PhoneNumber = "5916514144" },
            new ApplicationUser() { Id = "fc562933-2d03-4b76-ac44-1088cac87f1d", UserName = "DanielKing",  Email = "danielking@mail.com", PhoneNumber = "0307844410" },
            new ApplicationUser() { Id = "f9ad8f10-8342-476f-97a7-84381a33a35c", UserName = "MaryLewis",  Email = "marylewis@mail.com", PhoneNumber = "7497156307" },
            new ApplicationUser() { Id = "99f08ddf-d1d6-40be-bcec-79a358ea7cc1", UserName = "MichelleHenderson",  Email = "michellehenderson@mail.com", PhoneNumber = "9846299764" },
            new ApplicationUser() { Id = "bd278b4f-67fc-44ba-9461-4dae8108a44e", UserName = "GloriaJames",  Email = "gloriajames@mail.com", PhoneNumber = "7944029642" },
            new ApplicationUser() { Id = "3105a66a-0ac0-4c4b-9966-3449e04aac77", UserName = "ChloeMyers",  Email = "chloemyers@mail.com", PhoneNumber = "7996085547" },
            new ApplicationUser() { Id = "4ae8645c-3683-43c0-adba-acd4dfdd7849", UserName = "KatherineFoster",  Email = "katherinefoster@mail.com", PhoneNumber = "5073919772" },
            new ApplicationUser() { Id = "56149a33-f960-4f9a-9d1e-696e2b93acc4", UserName = "JanetHughes",  Email = "janethughes@mail.com", PhoneNumber = "0896811043" },
            new ApplicationUser() { Id = "53134ff7-14a4-4ab2-a36f-c0d009b6409b", UserName = "DianaGray",  Email = "dianagray@mail.com", PhoneNumber = "3913176631" },
            new ApplicationUser() { Id = "16ad6373-e0b6-414b-ba16-c618cb317fd3", UserName = "AnnaRoss",  Email = "annaross@mail.com", PhoneNumber = "1099169954" },
            new ApplicationUser() { Id = "6eadaa6e-0539-4f85-94bc-1cb5ae318f97", UserName = "LarryBennett",  Email = "larrybennett@mail.com", PhoneNumber = "0690283242" },
            new ApplicationUser() { Id = "8478051f-f1d2-4e03-8de0-fabf40dbc901", UserName = "KathleenMorgan",  Email = "kathleenmorgan@mail.com", PhoneNumber = "7773492989" },
            new ApplicationUser() { Id = "898186c7-a1a6-423a-b0ca-8f3455fcfbf0", UserName = "EmmaGordon",  Email = "emmagordon@mail.com", PhoneNumber = "0858678017" },
            new ApplicationUser() { Id = "a2300734-6798-4d31-ab87-c17c1163013c", UserName = "LindaWhite",  Email = "lindawhite@mail.com", PhoneNumber = "8701588696" },
            new ApplicationUser() { Id = "4b582485-5a88-4ce0-8322-771120917efd", UserName = "CarolKelly",  Email = "carolkelly@mail.com", PhoneNumber = "6998783239" },
            new ApplicationUser() { Id = "8c27f541-e38f-4afd-80bb-a6e41592cfd2", UserName = "RyanSanders",  Email = "ryansanders@mail.com", PhoneNumber = "1791453443" },
            new ApplicationUser() { Id = "aa6a0a19-36ba-446a-9b39-9cf81a2726ab", UserName = "SusanMitchell",  Email = "susanmitchell@mail.com", PhoneNumber = "3678092618" },
            new ApplicationUser() { Id = "8275b4d1-ef2e-4ed6-affb-a03f84962c93", UserName = "JohnDoe",  Email = "johndoe@mail.com", PhoneNumber = "0402120003" },
            new ApplicationUser() { Id = "32766f01-d898-46f7-9c45-56a12b573f23", UserName = "KarenGreen",  Email = "karengreen@mail.com", PhoneNumber = "0592817311" },
            new ApplicationUser() { Id = "3aaa85bc-f023-4db2-b07c-bda13a94f034", UserName = "CatherineRamirez",  Email = "catherineramirez@mail.com", PhoneNumber = "1823947507" },
            new ApplicationUser() { Id = "6cd4d51b-574a-4cc2-9d0b-4c0811610d12", UserName = "AmandaTaylor",  Email = "amandataylor@mail.com", PhoneNumber = "4290571504" },
            new ApplicationUser() { Id = "1f52e010-6bef-4078-9a72-6af5617e67c5", UserName = "DeborahRivera",  Email = "deborahrivera@mail.com", PhoneNumber = "9773753670" },
            new ApplicationUser() { Id = "ce36c5a9-251c-49be-9032-0225d6167866", UserName = "JackFord",  Email = "jackford@mail.com", PhoneNumber = "1386187552" },
            new ApplicationUser() { Id = "1667e9c3-2764-4bff-8989-0a7784aa5b6d", UserName = "ChristopherWilson",  Email = "christopherwilson@mail.com", PhoneNumber = "1459765423" },
            new ApplicationUser() { Id = "8fe522eb-89bb-4521-9742-4c930bcb669c", UserName = "LauraStewart",  Email = "laurastewart@mail.com", PhoneNumber = "7932600304" },
            new ApplicationUser() { Id = "17497924-ab14-4f92-9887-554aec702eb3", UserName = "StevenHill",  Email = "stevenhill@mail.com", PhoneNumber = "0096367714" },
            new ApplicationUser() { Id = "410de5e6-2a27-4aa0-8501-be43ba2eac7f", UserName = "EliJames",  Email = "elijames@mail.com", PhoneNumber = "8458224313" },
            new ApplicationUser() { Id = "71f1879d-4649-4989-bdf8-5e87b3c03a82", UserName = "OliviaCrawford",  Email = "oliviacrawford@mail.com", PhoneNumber = "9138790988" },
            new ApplicationUser() { Id = "0db83c49-1806-4167-ad43-f4eda2c348ac", UserName = "NicholasGutierrez",  Email = "nicholasgutierrez@mail.com", PhoneNumber = "5150570373" },
            new ApplicationUser() { Id = "3c56e653-a1bd-49b4-a794-d09352f0004a", UserName = "BrianPeterson",  Email = "brianpeterson@mail.com", PhoneNumber = "0043861509" },
            new ApplicationUser() { Id = "ea078289-4ad0-42a4-8b03-db0799789458", UserName = "KennethEdwards",  Email = "kennethedwards@mail.com", PhoneNumber = "5278199204" },
            new ApplicationUser() { Id = "0c20ba30-34bc-4159-998c-9ebe0aa62c93", UserName = "NancyWright",  Email = "nancywright@mail.com", PhoneNumber = "3618290035" },
            new ApplicationUser() { Id = "e28c980f-69ea-409d-9995-b71383137eaf", UserName = "PaulCollins",  Email = "paulcollins@mail.com", PhoneNumber = "1134418295" },
            new ApplicationUser() { Id = "a4ba32e0-f3d1-4a60-93d0-18b4c673b2e3", UserName = "JonathanKim",  Email = "jonathankim@mail.com", PhoneNumber = "3007189220" },
            new ApplicationUser() { Id = "cba165e7-f49c-4db0-a835-c4b7b1c30c0c", UserName = "PatrickTorres",  Email = "patricktorres@mail.com", PhoneNumber = "0522764023" },
            new ApplicationUser() { Id = "0c4cb023-9feb-43be-b620-c12d80465d8e", UserName = "GregoryBell",  Email = "gregorybell@mail.com", PhoneNumber = "7460397492" },
            new ApplicationUser() { Id = "b2723785-a9d4-417a-9c47-916518a3e592", UserName = "RachelWard",  Email = "rachelward@mail.com", PhoneNumber = "0902561618" },
            new ApplicationUser() { Id = "e9ae9bf3-9c54-4826-b366-fc0ece522ff8", UserName = "MasonHunter",  Email = "masonhunter@mail.com", PhoneNumber = "5272292433" },
            new ApplicationUser() { Id = "8493a3e9-9789-4421-abe1-cb33b3cd3ca1", UserName = "EdwardLong",  Email = "edwardlong@mail.com", PhoneNumber = "4827147265" },
            new ApplicationUser() { Id = "4f3de6f2-c294-4b64-a000-fabe228f0119", UserName = "AlexanderButler",  Email = "alexanderbutler@mail.com", PhoneNumber = "9916940971" },
            new ApplicationUser() { Id = "de224717-9b2f-4ffa-89da-a1d33519c5f8", UserName = "GeorgeParker",  Email = "georgeparker@mail.com", PhoneNumber = "4370740757" },
            new ApplicationUser() { Id = "8779e6bb-7348-446c-b8cf-6eff59494b4e", UserName = "JulieBarnes",  Email = "juliebarnes@mail.com", PhoneNumber = "8006073256" },
            new ApplicationUser() { Id = "88cee8f4-0f01-406a-8961-79438c016401", UserName = "ThomasAllen",  Email = "thomasallen@mail.com", PhoneNumber = "3278151172" },
            new ApplicationUser() { Id = "e4b647d9-c7b9-4892-9d32-4926a0fa8069", UserName = "ScottBailey",  Email = "scottbailey@mail.com", PhoneNumber = "9598276525" },
            new ApplicationUser() { Id = "3c7d5497-0ac0-44c4-a8b9-2111ec1e72f8", UserName = "JeffreyReyes",  Email = "jeffreyreyes@mail.com", PhoneNumber = "1593015962" },
            new ApplicationUser() { Id = "37d6903d-6252-4b20-a94b-7ce82e76b7f3", UserName = "ShirleyRogers",  Email = "shirleyrogers@mail.com", PhoneNumber = "0179281490" },
            new ApplicationUser() { Id = "bb8e48b8-b08d-4384-ab8e-c57c8306799e", UserName = "SamuelColeman",  Email = "samuelcoleman@mail.com", PhoneNumber = "2890557368" },
            new ApplicationUser() { Id = "22d767c3-4d6b-4bde-a4f3-011ee55220fb", UserName = "DonaldAdams",  Email = "donaldadams@mail.com", PhoneNumber = "4985506817" },
            new ApplicationUser() { Id = "8f88380c-3b3a-4d3d-a0b6-f514d57f2f96", UserName = "EricWatson",  Email = "ericwatson@mail.com", PhoneNumber = "5023716512" },
            new ApplicationUser() { Id = "cea1d9b5-3324-4f0e-8960-cdcd414a5984", UserName = "NoahGriffin",  Email = "noahgriffin@mail.com", PhoneNumber = "8393295418" },
            new ApplicationUser() { Id = "d788801f-1ee2-477f-a64a-b45e9e63261a", UserName = "DonnaEvans",  Email = "donnaevans@mail.com", PhoneNumber = "9193647331" },
            new ApplicationUser() { Id = "d91618ed-635a-4364-af20-b9b5cfd100ef", UserName = "JessicaTurner",  Email = "jessicaturner@mail.com", PhoneNumber = "8919722563" },
            new ApplicationUser() { Id = "0e83246c-5b5e-4e12-8a6d-fde43c90b000", UserName = "BenjaminPatterson",  Email = "benjaminpatterson@mail.com", PhoneNumber = "6911972539" },
            new ApplicationUser() { Id = "4e99e572-df6b-44db-8223-89762e05da25", UserName = "JudithGomez",  Email = "judithgomez@mail.com", PhoneNumber = "2504619188" },
            new ApplicationUser() { Id = "086fe946-cee5-42da-bbd2-b53efda922fc", UserName = "AmberSimmons",  Email = "ambersimmons@mail.com", PhoneNumber = "9986653536" },
            new ApplicationUser() { Id = "74ee3b21-6833-476b-83e1-5198874cb010", UserName = "GaryMorales",  Email = "garymorales@mail.com", PhoneNumber = "3522920397" },
            new ApplicationUser() { Id = "df17a7ee-0855-4504-89bd-016a07974373", UserName = "ChristinaStewart",  Email = "christinastewart@mail.com", PhoneNumber = "8648031940" },
            new ApplicationUser() { Id = "f37b2ed3-7ad0-481c-9221-b866ffac9d52", UserName = "VictoriaPowell",  Email = "victoriapowell@mail.com", PhoneNumber = "4421073276" },
            new ApplicationUser() { Id = "cdc3b029-f3c2-445d-988a-91cebdc2e0f4", UserName = "CynthiaReed",  Email = "cynthiareed@mail.com", PhoneNumber = "0926935883" },
            new ApplicationUser() { Id = "dc963caa-d0c9-437d-877d-60b1c058f2c1", UserName = "MatthewScott",  Email = "matthewscott@mail.com", PhoneNumber = "9680015467" },
            new ApplicationUser() { Id = "32c02f0e-0604-4845-98e2-48a2b86c6426", UserName = "StephenPerry",  Email = "stephenperry@mail.com", PhoneNumber = "5708684642" },
            new ApplicationUser() { Id = "2e2fd1a0-d447-48f4-a73e-d0d8186d6f7b", UserName = "MelissaHughes",  Email = "melissahughes@mail.com", PhoneNumber = "6880395707" },
            new ApplicationUser() { Id = "f98fbae1-5370-457c-814a-c94b856e886b", UserName = "SandraBaker",  Email = "sandrabaker@mail.com", PhoneNumber = "0718077530" },
            new ApplicationUser() { Id = "6e1e33f6-b141-4128-a84e-57725e434163", UserName = "AbigailSpencer",  Email = "abigailspencer@mail.com", PhoneNumber = "7632759120" },
            new ApplicationUser() { Id = "a08749b9-33dc-4e67-ad74-426974c47cc5", UserName = "PatriciaWalker",  Email = "patriciawalker@mail.com", PhoneNumber = "9429037751" },
            new ApplicationUser() { Id = "202ed390-c626-41dd-93a8-7b9066ad1524", UserName = "EthanBell",  Email = "ethanbell@mail.com", PhoneNumber = "7665367743" },
            new ApplicationUser() { Id = "07e9f788-9d12-4b3e-9c97-7407225c0ed6", UserName = "RuthWood",  Email = "ruthwood@mail.com", PhoneNumber = "7480196796" },
            new ApplicationUser() { Id = "ceaaee96-fe27-4b03-850c-db98005f71af", UserName = "BettyCampbell",  Email = "bettycampbell@mail.com", PhoneNumber = "6937180608" },
            new ApplicationUser() { Id = "4e4217f8-fea4-4874-b4dd-bd95defe911d", UserName = "MeganFox",  Email = "meganfox@mail.com", PhoneNumber = "5405879774" },
            new ApplicationUser() { Id = "303d8e0e-47ad-4a3d-8a59-a8d570f76f6e", UserName = "FrankDiaz",  Email = "frankdiaz@mail.com", PhoneNumber = "6185908779" },
            new ApplicationUser() { Id = "88c62f46-d261-4b11-8eca-8d95fbe63846", UserName = "JacobRussell",  Email = "jacobrussell@mail.com", PhoneNumber = "7546575608" },
            new ApplicationUser() { Id = "ee8ea390-4c91-40da-ac11-581f793cc4be", UserName = "MichaelBrown",  Email = "michaelbrown@mail.com", PhoneNumber = "9234036164" },
            new ApplicationUser() { Id = "bb39e852-3d64-4889-a686-1246a87673e7", UserName = "MarthaRichardson",  Email = "martharichardson@mail.com", PhoneNumber = "0615143087" },
            new ApplicationUser() { Id = "d901b00a-8642-4481-b046-f3faba0e12b0", UserName = "JaneSmith",  Email = "janesmith@mail.com", PhoneNumber = "5727092304" },
            new ApplicationUser() { Id = "9fa77923-b7ca-4990-92bf-d378c830b41c", UserName = "SarahThomas",  Email = "sarahthomas@mail.com", PhoneNumber = "4440319813" },
            new ApplicationUser() { Id = "894888fd-90f1-428c-be48-57428cd0b9f5", UserName = "MariaFlores",  Email = "mariaflores@mail.com", PhoneNumber = "4957124373" },
            new ApplicationUser() { Id = "40c29541-12e9-4799-88e0-d9241f3f3bf3", UserName = "LoganPalmer",  Email = "loganpalmer@mail.com", PhoneNumber = "7382559409" },
            new ApplicationUser() { Id = "4662ac0d-29dc-41b6-9c24-c1169dac3c74", UserName = "ElizabethYoung",  Email = "elizabethyoung@mail.com", PhoneNumber = "5002284471" },
            new ApplicationUser() { Id = "a35ea3b6-48c1-48b6-891d-4e844e503832", UserName = "CarolynOrtiz",  Email = "carolynortiz@mail.com", PhoneNumber = "5308599702" }
        };

        private static Subforum[] subforums = new Subforum[]
        {
            new Subforum
            {
                Name = "Cat Grooming",
                Users = users.Take(10).ToList(),
                CreatedOn = DateTime.UtcNow.AddDays(-2),
                Administrators = new ApplicationUser[]
                {
                    users[0],
                },
                Posts = new List<Post>()
                {
                    new Post
                    {
                        Title = "How do I groom my cat it always runs away?",
                        Text = "I can't convince my cat to stand still. It ignores treats and cannot be tricked to stand still!",
                        User = users[0],
                        CreatedOn = DateTime.UtcNow.AddDays(-2),
                        UpdatedOn = DateTime.UtcNow.AddDays(-2),
                        Votes = new List<PostVote>()
                        {
                            new PostVote()
                            {
                                User = users[2],
                                Type = PostVotes.Up,
                            },
                            new PostVote()
                            {
                                User = users[3],
                                Type = PostVotes.Up,
                            },
                            new PostVote()
                            {
                                User = users[4],
                                Type = PostVotes.Down,
                            },
                        },
                        Comments = new List<Comment>()
                        {
                            new Comment()
                            {
                                User = users[1],
                                CreatedOn = DateTime.UtcNow.AddDays(-1),
                                UpdatedOn = DateTime.UtcNow.AddDays(-1),
                                Text = "Have you tried giving it a toy to keep it distracted?",
                                Votes = new List<CommentVote>()
                                {
                                    new CommentVote
                                    {
                                        User = users[0],
                                        CreatedOn = DateTime.UtcNow.AddDays(-1),
                                        Type = CommentVotes.Down,
                                    }
                                },
                                Replies = new List<CommentReply>()
                                {
                                    new CommentReply
                                    {
                                        User = users[0],
                                        CreatedOn = DateTime.UtcNow.AddDays(-1),
                                        Text = "Yes, but it ignores that as well."
                                    }
                                }
                            }
                        },
                    }
                }
            },
            new Subforum
            {
                Name = "Workout tips",
                Users = users.Skip(10).Take(20).ToList(),
                CreatedOn = DateTime.UtcNow.AddDays(-4),
                Administrators = new ApplicationUser[]
                {
                    users[12],
                },
                Posts = new List<Post>()
                {
                    new Post
                    {
                        Title = "Are machines better than free weights?",
                        Text = "I am new to working out and cannot see a reason to use free weights over machines. Is there a reason why free weights are still popular?",
                        User = users[11],
                        CreatedOn = DateTime.UtcNow.AddDays(-2),
                        UpdatedOn = DateTime.UtcNow.AddDays(-2),
                        Votes = new List<PostVote>()
                        {
                            new PostVote()
                            {
                                User = users[11],
                                Type = PostVotes.Up,
                            },
                            new PostVote()
                            {
                                User = users[16],
                                Type = PostVotes.Down,
                            },
                            new PostVote()
                            {
                                User = users[18],
                                Type = PostVotes.Down,
                            },
                        },
                        Comments = new List<Comment>()
                        {
                            new Comment()
                            {
                                User = users[21],
                                CreatedOn = DateTime.UtcNow.AddDays(-1),
                                UpdatedOn = DateTime.UtcNow.AddDays(-1),
                                Text = "Free weights are useful because they activate more muscles than machines and therefore are better for building muscle in the long-run",
                                Votes = new List<CommentVote>()
                                {
                                    new CommentVote
                                    {
                                        User = users[11],
                                        CreatedOn = DateTime.UtcNow.AddDays(-3),
                                        Type = CommentVotes.Up,
                                    },
                                    new CommentVote
                                    {
                                        User = users[16],
                                        CreatedOn = DateTime.UtcNow.AddDays(-3),
                                        Type = CommentVotes.Up,
                                    },
                                    new CommentVote
                                    {
                                        User = users[18],
                                        CreatedOn = DateTime.UtcNow.AddDays(-3),
                                        Type = CommentVotes.Up,
                                    },
                                    new CommentVote
                                    {
                                        User = users[19],
                                        CreatedOn = DateTime.UtcNow.AddDays(-3),
                                        Type = CommentVotes.Up,
                                    },
                                    new CommentVote
                                    {
                                        User = users[25],
                                        CreatedOn = DateTime.UtcNow.AddDays(-2),
                                        Type = CommentVotes.Down,
                                    },
                                },
                                Replies = new List<CommentReply>()
                                {
                                    new CommentReply
                                    {
                                        User = users[11],
                                        CreatedOn = DateTime.UtcNow.AddDays(-3),
                                        UpdatedOn = DateTime.UtcNow.AddDays(-3),
                                        Text = "I didn't know that. Thank you!"
                                    },
                                    new CommentReply
                                    {
                                        User = users[25],
                                        CreatedOn = DateTime.UtcNow.AddDays(-2),
                                        UpdatedOn = DateTime.UtcNow.AddDays(-2),
                                        Text = "I disagree, machines are much better at everything"
                                    }
                                }
                            }
                        },
                    }
                }
            },
        };

        public SeedingService(ForumDbContext context,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<OperationResult> SeedAsync()
        {
            var operationResult = new OperationResult();
            if (await CheckAlreadySeededAsync())
            {
                operationResult.AddError(new Error(ErrorTypes.InternalServerError, "Database is already seeded!"));
                return operationResult;
            }

            ConfigureUsers();
            await context.Users.AddRangeAsync(users);

            await context.Subforums.AddRangeAsync(subforums);

            var seedEntity = new SeedEntity
            {
                SeededOn = DateTime.UtcNow
            };

            await context.SeedEntities.AddAsync(seedEntity);

            await context.SaveChangesAsync();

            return operationResult;
        }

        private void ConfigureUsers()
        {
            var password = "password";
            foreach (var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, password);
                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.UserName.ToUpper();
                user.ConcurrencyStamp = Guid.NewGuid().ToString();
                user.SecurityStamp = Guid.NewGuid().ToString();
            }
        }

        private async Task<bool> CheckAlreadySeededAsync()
        {
            return await context.SeedEntities.AnyAsync();
        }
    }
}
