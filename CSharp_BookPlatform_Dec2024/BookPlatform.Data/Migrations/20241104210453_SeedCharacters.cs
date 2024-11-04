using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCharacters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "IsDeleted", "IsSubmittedByUser", "Name" },
                values: new object[,]
                {
                    { new Guid("00c4832a-7ddb-4464-b10e-9f98a6d3a590"), false, false, "Dolores Umbridge" },
                    { new Guid("056664f2-dbbe-4a94-84bb-6e6ab022889a"), false, false, "Ron Weasley" },
                    { new Guid("06b295dd-5dd2-47ee-a528-56a370c5c493"), false, false, "George Weasley" },
                    { new Guid("0ac0e9bc-a2ec-4c5f-ab30-38d060749e19"), false, false, "Peregrine Took" },
                    { new Guid("0ed4d9f6-932a-46cb-81d7-94dc7a662d80"), false, false, "Cornelius Fudge" },
                    { new Guid("0edaf975-a570-4c63-836b-c133c9ae6f31"), false, false, "Horace Slughorn" },
                    { new Guid("0fb594e4-9666-4331-8268-52007fd0216d"), false, false, "Mr. Collins" },
                    { new Guid("106e8631-1838-4907-802c-767b9b97c35c"), false, false, "Narcissa Malfoy" },
                    { new Guid("13ff2d3c-8401-4cd9-b51c-149c84bd0d3b"), false, false, "Lydia Bennet" },
                    { new Guid("15390858-aa8d-4dc4-b3ef-80379bfd86ac"), false, false, "Cho Chang" },
                    { new Guid("16156c64-06be-4320-845c-589f4215fa23"), false, false, "Buckbeak" },
                    { new Guid("193f1aa1-259b-4b58-8e88-76f7d222bcc1"), false, false, "Quirinus Quirrell" },
                    { new Guid("2383ae23-f98c-45ae-bdba-bf7a16a2c947"), false, false, "Meriadoc Brandybuck" },
                    { new Guid("2608b54a-a09b-430b-8393-030656e99e1b"), false, false, "Bilbo Baggins" },
                    { new Guid("2b792faf-0d50-4b5f-9338-1cbf25f7b903"), false, false, "Mary Bennet" },
                    { new Guid("2da6a488-dbdb-486f-9aba-1aeb989030f1"), false, false, "Sauron" },
                    { new Guid("2ea5cbbf-b946-4f87-b5c4-2a7b1da441f2"), false, false, "Kitty Bennet" },
                    { new Guid("2ebd41f9-8e1b-4410-b413-453b8f8752f1"), false, false, "Severus Snape" },
                    { new Guid("3014036d-5440-42af-9c0c-bf4f0e7df5f2"), false, false, "Dudley Dursley" },
                    { new Guid("320ffbd1-a238-4b68-9d3d-f76254b05f62"), false, false, "Lucius Malfoy" },
                    { new Guid("34731f81-fc54-4076-8ed6-c8c1a5277642"), false, false, "Dobby" },
                    { new Guid("34a8dbd1-ceba-4182-818f-28bc6258fe43"), false, false, "Molly Weasley" },
                    { new Guid("36ada321-1060-4b8e-b16e-a95da878c1ca"), false, false, "Tommy" },
                    { new Guid("38417af6-9021-4171-ad9c-bb5919a0069d"), false, false, "Elizabeth Bennet" },
                    { new Guid("3902ffec-ba82-4ec5-b622-bf7669e161a4"), false, false, "Fleur Delacour" },
                    { new Guid("3a45fd62-4586-4538-b2e6-898b85e7367d"), false, false, "Arthur Weasley" },
                    { new Guid("3a46438a-5acc-42ae-a76c-2c3a4b1a03b5"), false, false, "Frodo Baggins" },
                    { new Guid("44f79007-7def-481c-962a-b4cc801b2ab5"), false, false, "Harry Potter" },
                    { new Guid("4628db53-9ea8-4bc1-b58f-da42db7bfb5d"), false, false, "Charlotte Lucas" },
                    { new Guid("4633a0ed-87e2-4e5e-b506-15c897db36d0"), false, false, "Gilderoy Lockhart" },
                    { new Guid("46f0b98a-e92b-4bde-9cee-420fde2da519"), false, false, "Samwise Gamgee" },
                    { new Guid("4e593fc4-6761-4f81-89dc-11b80c00a790"), false, false, "Pippi Longstocking" },
                    { new Guid("503a8548-2834-41b5-bfd9-31d5c7d2fe2a"), false, false, "Argus Filch" },
                    { new Guid("54a8c162-c190-4f03-b704-d04160622ce2"), false, false, "Garrick Ollivander" },
                    { new Guid("56406179-e1f3-44df-b26f-b2fe147da02f"), false, false, "Annika" },
                    { new Guid("5a0fafa4-d3dd-44fe-91d3-b4959d03429b"), false, false, "Rita Skeeter" },
                    { new Guid("5dc645bd-5017-4673-84d9-1376e5971316"), false, false, "Percy Weasley" },
                    { new Guid("6115e4ae-078c-4a36-9fda-e1321c4191f8"), false, false, "Elrond" },
                    { new Guid("61370023-3d73-4a8e-a88b-d42f9d311df0"), false, false, "Bartemius Crouch" },
                    { new Guid("649a427c-30d5-450b-9bb6-ae82a13636fc"), false, false, "Georgiana Darcy" },
                    { new Guid("6c434134-2395-4e7a-97f1-cec64b4feac9"), false, false, "Luna Lovegood" },
                    { new Guid("6eb7874e-48bc-443f-9a1f-5550c226300b"), false, false, "Igor Karkaroff" },
                    { new Guid("7113d840-3804-4d6f-91e0-3c28cc3bc9a0"), false, false, "Legolas" },
                    { new Guid("72d7e15f-dfd5-425f-bd42-23021f534ff1"), false, false, "Caroline Bingley" },
                    { new Guid("7394b47c-8d6b-40d3-8636-de4b9824ecb1"), false, false, "Faramir" },
                    { new Guid("766651f7-6dba-4060-bf93-2f93c25a0e0e"), false, false, "Kingsley Shacklebolt" },
                    { new Guid("78689e01-7a69-4ebe-9eeb-a92cea2b33dc"), false, false, "Mr. Darcy" },
                    { new Guid("7bbc7bc4-1ae4-4188-8e7e-5a766e205ed4"), false, false, "Arwen" },
                    { new Guid("8034f165-229c-49a2-92aa-82545bfc1302"), false, false, "Aunt Petunia" },
                    { new Guid("83910c4c-423c-4465-ab3a-fc220cd9e262"), false, false, "Fred Weasley" },
                    { new Guid("847940bf-9666-42b0-be19-bf7dfead1ded"), false, false, "Cedric Diggory" },
                    { new Guid("84e848cb-6877-47ab-9951-ce2115b34726"), false, false, "Sirius Black" },
                    { new Guid("868d163c-2211-402d-bd11-581fb6838f19"), false, false, "Bill Weasley" },
                    { new Guid("916bd001-ca7b-4a63-9d8c-49cfd5275052"), false, false, "Draco Malfoy" },
                    { new Guid("965224f8-ecf3-4bca-9677-bea52fc63ea7"), false, false, "Albus Dumbledore" },
                    { new Guid("96597e0d-a1ab-43a8-abfd-d2492c1633a5"), false, false, "Peter Pettigrew" },
                    { new Guid("966319c0-4992-4915-9a54-5eef45500ecb"), false, false, "Boromir" },
                    { new Guid("9b4b8256-27c4-4a0b-b7d2-7deeef3777fd"), false, false, "Neville Longbottom" },
                    { new Guid("9c228b07-50d8-45bd-8866-56b5fce02f78"), false, false, "Nymphadora Tonks" },
                    { new Guid("9d3bbb36-786c-4197-bf7f-bd4e296940ae"), false, false, "Oliver Wood" },
                    { new Guid("a382b48f-cb87-4a77-83e6-31450b2d5ed0"), false, false, "Charlie Weasley" },
                    { new Guid("a4088316-abca-45e9-b54f-c0e529a3dd74"), false, false, "Lady Catherine de Bourgh" },
                    { new Guid("a74939c2-0aae-4615-a0ff-45b5c1312fda"), false, false, "Galadriel" },
                    { new Guid("a7d5b484-5e01-4bdd-ae65-cefd41ec1851"), false, false, "Stan Shunpike" },
                    { new Guid("a82aeaa6-b9e9-43dd-81d5-a3754c71d91d"), false, false, "Hermione Granger" },
                    { new Guid("aaa8f5be-84e8-4b70-8034-1d22c54acdcb"), false, false, "Bartemius Crouch, Jr." },
                    { new Guid("ae1db45b-0ad7-49d2-b031-f55b01de6b3d"), false, false, "Gimli" },
                    { new Guid("af22a11d-ead2-49c6-b3ec-1de09bd82233"), false, false, "Mrs. Bennet" },
                    { new Guid("af8dd6e7-4ee3-4565-b432-7e48caaa8226"), false, false, "Remus Lupin" },
                    { new Guid("b0ec2257-6cc4-4361-9129-a38d3b434fa5"), false, false, "Golum" },
                    { new Guid("b5ae61a2-3372-4864-94bc-2741ab68044a"), false, false, "Lord Voldemort" },
                    { new Guid("b70f6a7c-83c1-426b-9df3-9bafc80471e4"), false, false, "Ginny Weasley" },
                    { new Guid("b8ce7f8f-2530-4e3e-8eab-3e28d4d34d2b"), false, false, "Uncle Vernon" },
                    { new Guid("b99ea61b-d7c7-4785-ba2a-a61e9789b5bf"), false, false, "Aragorn" },
                    { new Guid("c8947e19-a5a4-4f00-852e-2056dce11602"), false, false, "Gandalf" },
                    { new Guid("d4b1fa4f-a4dc-4383-b8da-ec1c372fdfa3"), false, false, "Minerva McGonagall" },
                    { new Guid("d7c7d8af-4c25-4b7a-b707-5197dad1d4dc"), false, false, "Hagrid" },
                    { new Guid("d80e48db-ff1f-4fc6-bde6-cd2391442957"), false, false, "Mr. Bingley" },
                    { new Guid("d9f1f065-1b40-44bc-889b-2073571c6098"), false, false, "Viktor Krum" },
                    { new Guid("da2b1f1c-dc8d-4d81-a664-f1a21550ee24"), false, false, "Jane Bennet" },
                    { new Guid("e1dfe43a-4102-4f35-99b3-932a1d5c9f8a"), false, false, "Alastor Moody" },
                    { new Guid("e4b530d2-5b60-4749-96f7-fbff38341cd2"), false, false, "Saruman" },
                    { new Guid("e55d3eb6-f95b-4e48-a1fd-4f9a871d61d2"), false, false, "Colin Creevey" },
                    { new Guid("ebb7e1f4-a3fb-4cc6-92bd-56a76859e797"), false, false, "Sybil Trelawney" },
                    { new Guid("ed7b5205-6fe8-423a-81ef-24be8ba1fe2d"), false, false, "Eowyn" },
                    { new Guid("f128e7ea-597f-49ed-8e7f-e62ee8fe5cbd"), false, false, "Kreacher" },
                    { new Guid("f3b028e4-bd85-4f6f-95ca-0f24805998e9"), false, false, "Mr. Bennet" },
                    { new Guid("f58488c4-32ab-4f8f-b2c8-8fbed256493a"), false, false, "Olympe Maxime" },
                    { new Guid("f78c4263-d5cd-4dcf-a2ab-91b412698b42"), false, false, "Bellatrix Lestrange" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("00c4832a-7ddb-4464-b10e-9f98a6d3a590"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("056664f2-dbbe-4a94-84bb-6e6ab022889a"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("06b295dd-5dd2-47ee-a528-56a370c5c493"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("0ac0e9bc-a2ec-4c5f-ab30-38d060749e19"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("0ed4d9f6-932a-46cb-81d7-94dc7a662d80"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("0edaf975-a570-4c63-836b-c133c9ae6f31"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("0fb594e4-9666-4331-8268-52007fd0216d"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("106e8631-1838-4907-802c-767b9b97c35c"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("13ff2d3c-8401-4cd9-b51c-149c84bd0d3b"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("15390858-aa8d-4dc4-b3ef-80379bfd86ac"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("16156c64-06be-4320-845c-589f4215fa23"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("193f1aa1-259b-4b58-8e88-76f7d222bcc1"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("2383ae23-f98c-45ae-bdba-bf7a16a2c947"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("2608b54a-a09b-430b-8393-030656e99e1b"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("2b792faf-0d50-4b5f-9338-1cbf25f7b903"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("2da6a488-dbdb-486f-9aba-1aeb989030f1"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("2ea5cbbf-b946-4f87-b5c4-2a7b1da441f2"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("2ebd41f9-8e1b-4410-b413-453b8f8752f1"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("3014036d-5440-42af-9c0c-bf4f0e7df5f2"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("320ffbd1-a238-4b68-9d3d-f76254b05f62"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("34731f81-fc54-4076-8ed6-c8c1a5277642"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("34a8dbd1-ceba-4182-818f-28bc6258fe43"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("36ada321-1060-4b8e-b16e-a95da878c1ca"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("38417af6-9021-4171-ad9c-bb5919a0069d"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("3902ffec-ba82-4ec5-b622-bf7669e161a4"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("3a45fd62-4586-4538-b2e6-898b85e7367d"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("3a46438a-5acc-42ae-a76c-2c3a4b1a03b5"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("44f79007-7def-481c-962a-b4cc801b2ab5"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("4628db53-9ea8-4bc1-b58f-da42db7bfb5d"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("4633a0ed-87e2-4e5e-b506-15c897db36d0"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("46f0b98a-e92b-4bde-9cee-420fde2da519"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("4e593fc4-6761-4f81-89dc-11b80c00a790"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("503a8548-2834-41b5-bfd9-31d5c7d2fe2a"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("54a8c162-c190-4f03-b704-d04160622ce2"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("56406179-e1f3-44df-b26f-b2fe147da02f"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("5a0fafa4-d3dd-44fe-91d3-b4959d03429b"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("5dc645bd-5017-4673-84d9-1376e5971316"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("6115e4ae-078c-4a36-9fda-e1321c4191f8"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("61370023-3d73-4a8e-a88b-d42f9d311df0"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("649a427c-30d5-450b-9bb6-ae82a13636fc"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("6c434134-2395-4e7a-97f1-cec64b4feac9"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("6eb7874e-48bc-443f-9a1f-5550c226300b"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("7113d840-3804-4d6f-91e0-3c28cc3bc9a0"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("72d7e15f-dfd5-425f-bd42-23021f534ff1"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("7394b47c-8d6b-40d3-8636-de4b9824ecb1"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("766651f7-6dba-4060-bf93-2f93c25a0e0e"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("78689e01-7a69-4ebe-9eeb-a92cea2b33dc"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("7bbc7bc4-1ae4-4188-8e7e-5a766e205ed4"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("8034f165-229c-49a2-92aa-82545bfc1302"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("83910c4c-423c-4465-ab3a-fc220cd9e262"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("847940bf-9666-42b0-be19-bf7dfead1ded"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("84e848cb-6877-47ab-9951-ce2115b34726"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("868d163c-2211-402d-bd11-581fb6838f19"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("916bd001-ca7b-4a63-9d8c-49cfd5275052"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("965224f8-ecf3-4bca-9677-bea52fc63ea7"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("96597e0d-a1ab-43a8-abfd-d2492c1633a5"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("966319c0-4992-4915-9a54-5eef45500ecb"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("9b4b8256-27c4-4a0b-b7d2-7deeef3777fd"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("9c228b07-50d8-45bd-8866-56b5fce02f78"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("9d3bbb36-786c-4197-bf7f-bd4e296940ae"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("a382b48f-cb87-4a77-83e6-31450b2d5ed0"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("a4088316-abca-45e9-b54f-c0e529a3dd74"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("a74939c2-0aae-4615-a0ff-45b5c1312fda"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("a7d5b484-5e01-4bdd-ae65-cefd41ec1851"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("a82aeaa6-b9e9-43dd-81d5-a3754c71d91d"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("aaa8f5be-84e8-4b70-8034-1d22c54acdcb"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("ae1db45b-0ad7-49d2-b031-f55b01de6b3d"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("af22a11d-ead2-49c6-b3ec-1de09bd82233"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("af8dd6e7-4ee3-4565-b432-7e48caaa8226"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("b0ec2257-6cc4-4361-9129-a38d3b434fa5"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("b5ae61a2-3372-4864-94bc-2741ab68044a"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("b70f6a7c-83c1-426b-9df3-9bafc80471e4"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("b8ce7f8f-2530-4e3e-8eab-3e28d4d34d2b"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("b99ea61b-d7c7-4785-ba2a-a61e9789b5bf"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("c8947e19-a5a4-4f00-852e-2056dce11602"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("d4b1fa4f-a4dc-4383-b8da-ec1c372fdfa3"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("d7c7d8af-4c25-4b7a-b707-5197dad1d4dc"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("d80e48db-ff1f-4fc6-bde6-cd2391442957"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("d9f1f065-1b40-44bc-889b-2073571c6098"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("da2b1f1c-dc8d-4d81-a664-f1a21550ee24"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("e1dfe43a-4102-4f35-99b3-932a1d5c9f8a"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("e4b530d2-5b60-4749-96f7-fbff38341cd2"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("e55d3eb6-f95b-4e48-a1fd-4f9a871d61d2"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("ebb7e1f4-a3fb-4cc6-92bd-56a76859e797"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("ed7b5205-6fe8-423a-81ef-24be8ba1fe2d"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("f128e7ea-597f-49ed-8e7f-e62ee8fe5cbd"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("f3b028e4-bd85-4f6f-95ca-0f24805998e9"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("f58488c4-32ab-4f8f-b2c8-8fbed256493a"));

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: new Guid("f78c4263-d5cd-4dcf-a2ab-91b412698b42"));
        }
    }
}
