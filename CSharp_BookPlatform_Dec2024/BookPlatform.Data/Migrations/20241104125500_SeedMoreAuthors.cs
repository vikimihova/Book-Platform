using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "FullName", "IsDeleted", "IsSubmittedByUser", "LastName" },
                values: new object[,]
                {
                    { new Guid("03999b94-b555-4c99-9614-6604576fb11f"), null, "Hans Christian Andersen", false, false, null },
                    { new Guid("08280cc9-3b18-46fe-9daa-c5f63c2a4bfd"), null, "Joseph Conrad", false, false, null },
                    { new Guid("0f90941f-bcc4-4d12-a297-60f626ae9959"), null, "Rumi", false, false, null },
                    { new Guid("10bacfde-3d47-479e-9460-1e149379ab24"), null, "Juan Rulfo", false, false, null },
                    { new Guid("1ba0d789-60ac-4301-b748-a932f3eafcac"), null, "Louis-Ferdinand Céline", false, false, null },
                    { new Guid("1be273be-802e-4401-a0ed-d060ecb0b2b8"), null, "Virginia Woolf", false, false, null },
                    { new Guid("2396856b-a432-46b0-ac81-18158d8469e7"), null, "Vyasa", false, false, null },
                    { new Guid("2502f281-4153-411d-8919-7b12e9db42be"), null, "Vladimir Nabokov", false, false, null },
                    { new Guid("26a7a0d8-2642-436f-85c5-97105564aa3b"), null, "Federico García Lorca", false, false, null },
                    { new Guid("29a77734-8c17-4b32-a843-4c07c652bf50"), null, "Saadi", false, false, null },
                    { new Guid("29efd3c9-42f5-4d95-9a12-061aae946552"), null, "Jonathan Swift", false, false, null },
                    { new Guid("2c884fda-240a-4004-b652-ada0225959a3"), null, "Giacomo Leopardi", false, false, null },
                    { new Guid("2cd4eab2-904a-4261-bb1a-e7d4c983f99c"), null, "Murasaki Shikibu", false, false, null },
                    { new Guid("2e065f61-7fe7-43b8-9c7c-d98d15f1469d"), null, "Albert Camus", false, false, null },
                    { new Guid("2e6af862-bf5c-486e-ba1f-ab6289500526"), null, "Samuel Beckett", false, false, null },
                    { new Guid("32ee9267-8bed-401d-8f04-29fca9f27982"), null, "Naguib Mahfouz", false, false, null },
                    { new Guid("34034575-e90c-42e2-9609-47cfcc32867c"), null, "William Faulkner", false, false, null },
                    { new Guid("3512d0c8-c7de-49b1-990c-6048a08db9ae"), null, "Herman Melville", false, false, null },
                    { new Guid("38201b6f-4feb-49ad-9a8c-62903f263b31"), null, "Halldór Laxness", false, false, null },
                    { new Guid("38faa530-09c5-45ed-8120-7cad1f3da2af"), null, "Lu Xun", false, false, null },
                    { new Guid("412fecf6-6740-4091-bba9-d59c3941c01e"), null, "Italo Svevo", false, false, null },
                    { new Guid("437c5af6-d832-4a17-b5f8-fc58ff353a1b"), null, "Johann Wolfgang von Goethe", false, false, null },
                    { new Guid("440bc913-e0c1-45c0-a517-0e7f30066d7a"), null, "Laurence Sterne", false, false, null },
                    { new Guid("4a0b3917-56f9-481f-8207-5b95c9d66d0b"), null, "Charles Dickens", false, false, null },
                    { new Guid("4ca1b43b-d451-4332-b75e-c6acd00b5bf9"), null, "Walt Whitman", false, false, null },
                    { new Guid("4d37e72b-5cc1-47a7-a537-88b67be5e5b5"), null, "George Orwell", false, false, null },
                    { new Guid("50aaf6a5-cc2f-4b3a-ae6d-4c2d8a8d4345"), null, "Günter Grass", false, false, null },
                    { new Guid("51717429-e61d-4e99-87a0-a6f4977979b3"), null, "Jane Austen", false, false, null },
                    { new Guid("52a260da-0881-4207-89f8-fb47d4d5e0a5"), null, "José Saramago", false, false, null },
                    { new Guid("543b212b-1be0-4536-810f-8b724e5c1145"), null, "Giovanni Boccaccio", false, false, null },
                    { new Guid("5d0a653d-cdb1-4af7-a3c8-744db00454fe"), null, "Homer", false, false, null },
                    { new Guid("61e4d82e-9884-4ef3-9e32-b12b331072c3"), null, "Thomas Mann", false, false, null },
                    { new Guid("6251bd75-a73d-46e0-b488-2431a7204e01"), null, "James Joyce", false, false, null },
                    { new Guid("6493dea5-2976-4495-9c38-9054b177c014"), null, "Gabriel García Márquez", false, false, null },
                    { new Guid("664db420-515a-4a40-8b60-25d2ab0fcdf0"), null, "Ernest Hemingway", false, false, null },
                    { new Guid("69a54e30-11cd-495c-a222-2b0fd492b8a4"), null, "J.K. Rowling", false, false, null },
                    { new Guid("6a4e2654-ad55-4d6b-b19d-86ca3dac4623"), null, "Virgil", false, false, null },
                    { new Guid("6b2a21f8-1aaf-4c80-adf8-2fe78b13d5d4"), null, "Geoffrey Chaucer", false, false, null },
                    { new Guid("6b381fd8-0586-4ae7-b32d-74be21dd499f"), null, "Ralph Ellison", false, false, null },
                    { new Guid("6c039d83-3daf-4fff-b345-8140dd3bac9a"), null, "Michel de Montaigne", false, false, null },
                    { new Guid("70e51876-1710-4860-9857-0bb2e9904cc4"), null, "Elsa Morante", false, false, null },
                    { new Guid("74badb95-e1b4-4a93-b808-a7196b2f1ec0"), null, "Edgar Allan Poe", false, false, null },
                    { new Guid("77889f64-1fb7-4a68-9ced-0661b24b9ecc"), null, "Ovid", false, false, null },
                    { new Guid("79623f65-bc2e-4457-b2c5-727e3692ce4c"), null, "Valmiki", false, false, null },
                    { new Guid("7f3465be-ccf8-492b-9e71-65d5cfee6cbb"), null, "Anton Chekhov", false, false, null },
                    { new Guid("7fd161ef-ce76-44e5-8cc8-d827b87c35fd"), null, "Franz Kafka", false, false, null },
                    { new Guid("80861939-7139-41fd-bb49-9bbb46eccd7c"), null, "Nikolai Gogol", false, false, null },
                    { new Guid("81f60cb8-24e9-4b6a-8b42-5c201685b8f7"), null, "Gustave Flaubert", false, false, null },
                    { new Guid("85446113-6f3b-4844-b986-313dcdcf0e46"), null, "Paul Celan", false, false, null },
                    { new Guid("8747d54d-028a-491c-8afe-ddd00881fc94"), null, "Tayeb Salih", false, false, null },
                    { new Guid("87566daa-3dfc-4bf9-bfab-0eee380797c6"), null, "Mark Twain", false, false, null },
                    { new Guid("8c44ee9a-ff60-4a6d-aca9-763e5ff321a0"), null, "Leo Tolstoy", false, false, null },
                    { new Guid("8fb2fb8b-50b3-40da-9a5f-74f531458cc0"), null, "François Rabelais", false, false, null },
                    { new Guid("903275a0-f1e5-43df-b733-e9033ff47da8"), null, "George Eliot", false, false, null },
                    { new Guid("91c4f2a6-8b65-4ab6-817d-c5d7df909293"), null, "Denis Diderot", false, false, null },
                    { new Guid("9d866759-0a3a-4076-bc1d-1d39e7accbfe"), null, "William Shakespeare", false, false, null },
                    { new Guid("a3933b58-da2f-4d9e-ac1b-971406f490ab"), null, "Toni Morrison", false, false, null },
                    { new Guid("a544cf63-7c21-44ba-a3d1-8de4ba179eeb"), null, "Nikos Kazantzakis", false, false, null },
                    { new Guid("a72d1257-ee68-402c-9b89-9adae0b89875"), null, "Emily Brontë", false, false, null },
                    { new Guid("ae927ed3-5eab-451c-8615-9d358e5f6796"), null, "Stendhal", false, false, null },
                    { new Guid("b5e82218-6746-4eee-a9df-e5788b43ae2a"), null, "Alfred Döblin", false, false, null },
                    { new Guid("b5eb741e-352b-448d-a96c-6dc42387ef90"), null, "Sophocles", false, false, null },
                    { new Guid("ba1a41ba-d43e-4fe4-8cd6-8e2b3cb4e0ec"), null, "Honoré de Balzac", false, false, null },
                    { new Guid("bc9cf5b3-e5aa-4e00-a57b-28e4155950d6"), null, "Fernando Pessoa", false, false, null },
                    { new Guid("bfcfc8fe-b67c-42a0-9feb-e0ded4ec4cfb"), null, "J.R.R. Tolkien", false, false, null },
                    { new Guid("c48ecdc4-d0e5-40d5-ab5c-1ebe64676681"), null, "Jorge Luis Borges", false, false, null },
                    { new Guid("c6f4f541-6d5c-4fb6-bc74-f2a26d03e7c0"), null, "Dante Alighieri", false, false, null },
                    { new Guid("c936f129-ee96-4a6d-a040-18b3a1113bac"), null, "D. H. Lawrence", false, false, null },
                    { new Guid("caf6affa-3ebf-4e66-b54f-bda1cc41e3b0"), null, "Miguel de Cervantes", false, false, null },
                    { new Guid("cb88244d-f646-431c-8d1a-1f26c5f4c763"), null, "Fyodor Dostoevsky", false, false, null },
                    { new Guid("d0ba348e-e67a-4f6b-b386-278c2a113fe1"), null, "João Guimarães Rosa", false, false, null },
                    { new Guid("d227dbcc-7865-4755-9dea-c0799ffb58c7"), null, "Knut Hamsun", false, false, null },
                    { new Guid("d55d891e-67e2-46f7-a5c1-1e8580ba6624"), null, "Euripides", false, false, null },
                    { new Guid("d88a6abf-03b0-4a01-b0c5-938a926d7ac8"), null, "Kālidāsa", false, false, null },
                    { new Guid("e00c4a94-045f-4921-98be-e58c6e6e7827"), null, "Unknown", false, false, null },
                    { new Guid("e1aa929c-928d-4b3e-8a43-03e49630b4c2"), null, "Doris Lessing", false, false, null },
                    { new Guid("e472606b-b39f-49e7-8777-622f3792dccf"), null, "Yasunari Kawabata", false, false, null },
                    { new Guid("e7a3a8a2-2fb9-4e0f-85ce-bfe681322224"), null, "Marguerite Yourcenar", false, false, null },
                    { new Guid("e9594109-079c-4ac7-b67b-65bcd2a52497"), null, "Marcel Proust", false, false, null },
                    { new Guid("ebe93f0f-3dba-4b94-808c-6f9606b6a06f"), null, "Salman Rushdie", false, false, null },
                    { new Guid("ee74589f-be60-45af-b0e2-ba324f1479f7"), null, "Astrid Lindgren", false, false, null },
                    { new Guid("f022d88f-e812-427e-b4f5-087b002d10a2"), null, "Henrik Ibsen", false, false, null },
                    { new Guid("fcd666e4-33e4-4929-9e32-9089b4d75347"), null, "Robert Musil", false, false, null },
                    { new Guid("fffafc05-aeb9-450d-b27b-b3b4b92a0d4d"), null, "Chinua Achebe", false, false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("03999b94-b555-4c99-9614-6604576fb11f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("08280cc9-3b18-46fe-9daa-c5f63c2a4bfd"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0f90941f-bcc4-4d12-a297-60f626ae9959"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("10bacfde-3d47-479e-9460-1e149379ab24"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("1ba0d789-60ac-4301-b748-a932f3eafcac"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("1be273be-802e-4401-a0ed-d060ecb0b2b8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2396856b-a432-46b0-ac81-18158d8469e7"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2502f281-4153-411d-8919-7b12e9db42be"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("26a7a0d8-2642-436f-85c5-97105564aa3b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("29a77734-8c17-4b32-a843-4c07c652bf50"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("29efd3c9-42f5-4d95-9a12-061aae946552"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2c884fda-240a-4004-b652-ada0225959a3"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2cd4eab2-904a-4261-bb1a-e7d4c983f99c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2e065f61-7fe7-43b8-9c7c-d98d15f1469d"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2e6af862-bf5c-486e-ba1f-ab6289500526"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("32ee9267-8bed-401d-8f04-29fca9f27982"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("34034575-e90c-42e2-9609-47cfcc32867c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("3512d0c8-c7de-49b1-990c-6048a08db9ae"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("38201b6f-4feb-49ad-9a8c-62903f263b31"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("38faa530-09c5-45ed-8120-7cad1f3da2af"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("412fecf6-6740-4091-bba9-d59c3941c01e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("437c5af6-d832-4a17-b5f8-fc58ff353a1b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("440bc913-e0c1-45c0-a517-0e7f30066d7a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4a0b3917-56f9-481f-8207-5b95c9d66d0b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4ca1b43b-d451-4332-b75e-c6acd00b5bf9"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4d37e72b-5cc1-47a7-a537-88b67be5e5b5"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("50aaf6a5-cc2f-4b3a-ae6d-4c2d8a8d4345"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("51717429-e61d-4e99-87a0-a6f4977979b3"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("52a260da-0881-4207-89f8-fb47d4d5e0a5"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("543b212b-1be0-4536-810f-8b724e5c1145"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5d0a653d-cdb1-4af7-a3c8-744db00454fe"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("61e4d82e-9884-4ef3-9e32-b12b331072c3"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6251bd75-a73d-46e0-b488-2431a7204e01"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6493dea5-2976-4495-9c38-9054b177c014"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("664db420-515a-4a40-8b60-25d2ab0fcdf0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("69a54e30-11cd-495c-a222-2b0fd492b8a4"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6a4e2654-ad55-4d6b-b19d-86ca3dac4623"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6b2a21f8-1aaf-4c80-adf8-2fe78b13d5d4"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6b381fd8-0586-4ae7-b32d-74be21dd499f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6c039d83-3daf-4fff-b345-8140dd3bac9a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("70e51876-1710-4860-9857-0bb2e9904cc4"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("74badb95-e1b4-4a93-b808-a7196b2f1ec0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("77889f64-1fb7-4a68-9ced-0661b24b9ecc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("79623f65-bc2e-4457-b2c5-727e3692ce4c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7f3465be-ccf8-492b-9e71-65d5cfee6cbb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7fd161ef-ce76-44e5-8cc8-d827b87c35fd"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("80861939-7139-41fd-bb49-9bbb46eccd7c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("81f60cb8-24e9-4b6a-8b42-5c201685b8f7"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("85446113-6f3b-4844-b986-313dcdcf0e46"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8747d54d-028a-491c-8afe-ddd00881fc94"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("87566daa-3dfc-4bf9-bfab-0eee380797c6"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8c44ee9a-ff60-4a6d-aca9-763e5ff321a0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8fb2fb8b-50b3-40da-9a5f-74f531458cc0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("903275a0-f1e5-43df-b733-e9033ff47da8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("91c4f2a6-8b65-4ab6-817d-c5d7df909293"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("9d866759-0a3a-4076-bc1d-1d39e7accbfe"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a3933b58-da2f-4d9e-ac1b-971406f490ab"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a544cf63-7c21-44ba-a3d1-8de4ba179eeb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a72d1257-ee68-402c-9b89-9adae0b89875"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ae927ed3-5eab-451c-8615-9d358e5f6796"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b5e82218-6746-4eee-a9df-e5788b43ae2a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b5eb741e-352b-448d-a96c-6dc42387ef90"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ba1a41ba-d43e-4fe4-8cd6-8e2b3cb4e0ec"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("bc9cf5b3-e5aa-4e00-a57b-28e4155950d6"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("bfcfc8fe-b67c-42a0-9feb-e0ded4ec4cfb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("c48ecdc4-d0e5-40d5-ab5c-1ebe64676681"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("c6f4f541-6d5c-4fb6-bc74-f2a26d03e7c0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("c936f129-ee96-4a6d-a040-18b3a1113bac"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("caf6affa-3ebf-4e66-b54f-bda1cc41e3b0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("cb88244d-f646-431c-8d1a-1f26c5f4c763"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d0ba348e-e67a-4f6b-b386-278c2a113fe1"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d227dbcc-7865-4755-9dea-c0799ffb58c7"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d55d891e-67e2-46f7-a5c1-1e8580ba6624"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d88a6abf-03b0-4a01-b0c5-938a926d7ac8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e00c4a94-045f-4921-98be-e58c6e6e7827"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e1aa929c-928d-4b3e-8a43-03e49630b4c2"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e472606b-b39f-49e7-8777-622f3792dccf"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e7a3a8a2-2fb9-4e0f-85ce-bfe681322224"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e9594109-079c-4ac7-b67b-65bcd2a52497"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ebe93f0f-3dba-4b94-808c-6f9606b6a06f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ee74589f-be60-45af-b0e2-ba324f1479f7"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("f022d88f-e812-427e-b4f5-087b002d10a2"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("fcd666e4-33e4-4929-9e32-9089b4d75347"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("fffafc05-aeb9-450d-b27b-b3b4b92a0d4d"));
        }
    }
}
