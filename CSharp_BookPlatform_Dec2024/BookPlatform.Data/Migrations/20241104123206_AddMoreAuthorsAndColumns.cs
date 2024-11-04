using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreAuthorsAndColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("00d4bd5a-1c59-47f4-82f8-f40640a0b59a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("076f8fc2-9e07-48bf-ade1-1941f6113b5b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0dbd3d1e-2de5-4ae7-a588-3916c90caac4"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("13b6a375-94a1-448b-a852-9c417d64219e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("142c9661-d2ff-4f9a-83aa-dabfca26ed61"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("16ba9d4a-24bf-4989-83fa-91559d10db0d"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("1b2206de-ba2a-4aae-9ef0-31ec64ea5afb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("1bf03ffc-89a6-448f-9036-8d206171ad3b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("1e1c7dc3-b968-4512-9893-6ab74ca995f9"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2529db97-63f5-47fb-be8c-572ef70cf006"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("25ab0068-0a2e-4c0d-ac88-2761e4a7e528"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2b0bbb19-1bbd-4234-a2d4-66ad540efbe6"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2cbfec6b-6179-42f3-b1fa-4a95cc4acfa8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("32366d19-9aae-4189-80a4-1020fb23d59e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("346fbc5c-82b0-4d34-87b2-65d516c7f274"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("351859cf-a79c-4961-be84-5bf1ab8441fe"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("378e776a-6fff-4886-ba19-89b6bb2ac6e6"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("38b02682-deba-4ad0-9fe8-22c8415f39bc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("3edc7ed9-edcc-4a0e-837a-521ce3e618f9"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("3eeab217-e009-478b-928e-993ce447d9f3"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("417665f7-0684-4482-a1ab-a0ee3f067686"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("43a94ab0-4457-4ef3-b31f-50d9acd71b09"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4a3c1349-f3b8-470d-a7b4-825b54132965"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4e516a05-f82f-4584-81db-667f065c70b1"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("523c7a9d-d6d5-42a1-bbb4-d23f79bc55d7"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5530e7d3-65f2-4af9-b83c-2dc2f77b4bbf"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("59634636-8af4-4dd7-82fa-bc17043cb4f1"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5da48cec-1f36-4666-921d-43b90357226f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5e1894d2-dc95-4cdc-b378-4882c4da7808"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5eb629c4-bdba-4fb7-9242-96028af47077"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5f9b01b2-714a-4254-a039-d7cbb282bf2a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("63f316b6-af65-42b5-ba02-0df18b618cdb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("63fae9d7-bd92-4afa-930d-80377f54cbbc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("6633bdb4-35c2-4294-b15e-3ddd8595b0ae"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("67dc59ad-3ff7-4906-bd38-f35f593e1c61"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("711ca5c9-304c-47e5-b0a1-357680251fdf"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("71b9c979-95a7-4e58-94b2-505c631d8ea2"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("72a2ed8d-354a-47b5-9219-d386ff5844d6"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("72ea3019-fe4b-4c2c-b919-fc07334c0c3f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("783bbacc-bc65-470e-a53e-86869c61698c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7bc3ef5d-ab75-4ce5-bd2d-efe7de0d3ca8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7f09a98a-b777-4a87-b31f-77696dac5c12"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("84a58da6-15d5-4838-8df8-1bf9708c17bc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("893b9552-f04d-482a-9323-b030158aafc6"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8e887eb6-fec4-4d2b-98ea-9b4b31ed519d"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8f7f3912-ce99-434f-a23d-8fa5b848acb8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("90d3e8d2-28e9-46fc-8ba5-12816eaecc6d"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("95d41a49-4a3b-478a-8694-a7a159eda369"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("96948e9f-b9a2-48a1-b614-92c08a8b3b4c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("9970c321-7e1b-45ca-a241-b8e04b34575e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("9c15c4e2-d17e-4311-92f2-42035e3db1ef"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("9e5b1956-e4cc-4de8-8316-0fbe83c9c2e5"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a20b0c8f-e2cc-4b5b-85f2-c3a570796e74"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a3d4426f-67f8-43b1-8901-750bcea04e20"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a714c1c6-1705-478f-8231-b293e111995e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ab273ba4-c96c-4092-81c1-750ee3c71dbb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ae2bd410-b110-40c4-b56f-31be18fc1923"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("af0828c7-f49e-4d1b-a888-4e06335dcd04"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("af7982e7-da2d-4bc2-a5aa-968752029299"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b13f8b26-6a6e-4574-a2bc-ae28caf99124"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b28d9964-eabe-4a33-911e-a861b6b71a70"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b8894cea-2e38-4713-8b97-06d7bbf9c8aa"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ba51caa3-77fe-43b7-ba2e-d78b13cfafc2"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("bc851c3a-6ffa-4b74-bd8e-36ba2b04d4fb"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("be5885ea-c04f-4c48-91a4-31d9bba8d2c2"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("c264ed2e-b98f-4f00-b055-65a327f1b2e0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("c3a5174b-5a8a-4076-9a6f-3b2eda4744ba"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("c6d892db-d741-42a6-a4e4-d23fcc4c4739"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ca4895a4-5aad-4355-975a-efbdd1ff1715"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ce5f3664-f332-4bcd-a7f9-b413733e94dc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d556a20d-6dba-4494-b0ec-ae2e82eacf3b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("da83a1a0-6d2d-431d-9fd0-05d7b13b3f79"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("dbe21766-6473-4ed9-a130-17efc9f55310"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ddf5c81b-6351-48c0-863d-adab65acaefc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("de81ae05-a296-450b-aaa9-61d04448930e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e09e0cf4-f001-49b2-8486-698c970991d3"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("eadcdc57-979f-4278-8f3f-0d154a09abed"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ec43876e-7cac-436c-a115-55642490e19b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ecad5f48-371a-4a60-8e5e-bbf03347c2a3"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ef7c9244-7559-4fc9-a42b-6c1f6050a236"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("f7df9789-4cd7-49d3-af01-ebe6707b138a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("fbec15ce-53b0-442a-97f7-9dd03d6a43c2"));

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmittedByUser",
                table: "Characters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "BooksApplicationUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Date on which the user added the book to a reading list");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmittedByUser",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmittedByUser",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmittedByUser",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "BooksApplicationUsers");

            migrationBuilder.DropColumn(
                name: "IsSubmittedByUser",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsSubmittedByUser",
                table: "Authors");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "FullName", "IsDeleted", "LastName" },
                values: new object[,]
                {
                    { new Guid("00d4bd5a-1c59-47f4-82f8-f40640a0b59a"), null, "Italo Svevo", false, null },
                    { new Guid("076f8fc2-9e07-48bf-ade1-1941f6113b5b"), null, "Giovanni Boccaccio", false, null },
                    { new Guid("0dbd3d1e-2de5-4ae7-a588-3916c90caac4"), null, "Marcel Proust", false, null },
                    { new Guid("13b6a375-94a1-448b-a852-9c417d64219e"), null, "Stendhal", false, null },
                    { new Guid("142c9661-d2ff-4f9a-83aa-dabfca26ed61"), null, "Robert Musil", false, null },
                    { new Guid("16ba9d4a-24bf-4989-83fa-91559d10db0d"), null, "William Shakespeare", false, null },
                    { new Guid("1b2206de-ba2a-4aae-9ef0-31ec64ea5afb"), null, "Charles Dickens", false, null },
                    { new Guid("1bf03ffc-89a6-448f-9036-8d206171ad3b"), null, "Chinua Achebe", false, null },
                    { new Guid("1e1c7dc3-b968-4512-9893-6ab74ca995f9"), null, "Virgil", false, null },
                    { new Guid("2529db97-63f5-47fb-be8c-572ef70cf006"), null, "Valmiki", false, null },
                    { new Guid("25ab0068-0a2e-4c0d-ac88-2761e4a7e528"), null, "Sophocles", false, null },
                    { new Guid("2b0bbb19-1bbd-4234-a2d4-66ad540efbe6"), null, "Denis Diderot", false, null },
                    { new Guid("2cbfec6b-6179-42f3-b1fa-4a95cc4acfa8"), null, "Alfred Döblin", false, null },
                    { new Guid("32366d19-9aae-4189-80a4-1020fb23d59e"), null, "Gustave Flaubert", false, null },
                    { new Guid("346fbc5c-82b0-4d34-87b2-65d516c7f274"), null, "Astrid Lindgren", false, null },
                    { new Guid("351859cf-a79c-4961-be84-5bf1ab8441fe"), null, "Nikolai Gogol", false, null },
                    { new Guid("378e776a-6fff-4886-ba19-89b6bb2ac6e6"), null, "José Saramago", false, null },
                    { new Guid("38b02682-deba-4ad0-9fe8-22c8415f39bc"), null, "Dante Alighieri", false, null },
                    { new Guid("3edc7ed9-edcc-4a0e-837a-521ce3e618f9"), null, "Hans Christian Andersen", false, null },
                    { new Guid("3eeab217-e009-478b-928e-993ce447d9f3"), null, "João Guimarães Rosa", false, null },
                    { new Guid("417665f7-0684-4482-a1ab-a0ee3f067686"), null, "Samuel Beckett", false, null },
                    { new Guid("43a94ab0-4457-4ef3-b31f-50d9acd71b09"), null, "Henrik Ibsen", false, null },
                    { new Guid("4a3c1349-f3b8-470d-a7b4-825b54132965"), null, "William Faulkner", false, null },
                    { new Guid("4e516a05-f82f-4584-81db-667f065c70b1"), null, "Ralph Ellison", false, null },
                    { new Guid("523c7a9d-d6d5-42a1-bbb4-d23f79bc55d7"), null, "Franz Kafka", false, null },
                    { new Guid("5530e7d3-65f2-4af9-b83c-2dc2f77b4bbf"), null, "Johann Wolfgang von Goethe", false, null },
                    { new Guid("59634636-8af4-4dd7-82fa-bc17043cb4f1"), null, "Joseph Conrad", false, null },
                    { new Guid("5da48cec-1f36-4666-921d-43b90357226f"), null, "Marguerite Yourcenar", false, null },
                    { new Guid("5e1894d2-dc95-4cdc-b378-4882c4da7808"), null, "Euripides", false, null },
                    { new Guid("5eb629c4-bdba-4fb7-9242-96028af47077"), null, "Thomas Mann", false, null },
                    { new Guid("5f9b01b2-714a-4254-a039-d7cbb282bf2a"), null, "Michel de Montaigne", false, null },
                    { new Guid("63f316b6-af65-42b5-ba02-0df18b618cdb"), null, "Laurence Sterne", false, null },
                    { new Guid("63fae9d7-bd92-4afa-930d-80377f54cbbc"), null, "Virginia Woolf", false, null },
                    { new Guid("6633bdb4-35c2-4294-b15e-3ddd8595b0ae"), null, "George Eliot", false, null },
                    { new Guid("67dc59ad-3ff7-4906-bd38-f35f593e1c61"), null, "François Rabelais", false, null },
                    { new Guid("711ca5c9-304c-47e5-b0a1-357680251fdf"), null, "George Orwell", false, null },
                    { new Guid("71b9c979-95a7-4e58-94b2-505c631d8ea2"), null, "Paul Celan", false, null },
                    { new Guid("72a2ed8d-354a-47b5-9219-d386ff5844d6"), null, "Jonathan Swift", false, null },
                    { new Guid("72ea3019-fe4b-4c2c-b919-fc07334c0c3f"), null, "Vladimir Nabokov", false, null },
                    { new Guid("783bbacc-bc65-470e-a53e-86869c61698c"), null, "Edgar Allan Poe", false, null },
                    { new Guid("7bc3ef5d-ab75-4ce5-bd2d-efe7de0d3ca8"), null, "Albert Camus", false, null },
                    { new Guid("7f09a98a-b777-4a87-b31f-77696dac5c12"), null, "Yasunari Kawabata", false, null },
                    { new Guid("84a58da6-15d5-4838-8df8-1bf9708c17bc"), null, "Anton Chekhov", false, null },
                    { new Guid("893b9552-f04d-482a-9323-b030158aafc6"), null, "Geoffrey Chaucer", false, null },
                    { new Guid("8e887eb6-fec4-4d2b-98ea-9b4b31ed519d"), null, "Kālidāsa", false, null },
                    { new Guid("8f7f3912-ce99-434f-a23d-8fa5b848acb8"), null, "Unknown", false, null },
                    { new Guid("90d3e8d2-28e9-46fc-8ba5-12816eaecc6d"), null, "Günter Grass", false, null },
                    { new Guid("95d41a49-4a3b-478a-8694-a7a159eda369"), null, "Murasaki Shikibu", false, null },
                    { new Guid("96948e9f-b9a2-48a1-b614-92c08a8b3b4c"), null, "Herman Melville", false, null },
                    { new Guid("9970c321-7e1b-45ca-a241-b8e04b34575e"), null, "Tayeb Salih", false, null },
                    { new Guid("9c15c4e2-d17e-4311-92f2-42035e3db1ef"), null, "Walt Whitman", false, null },
                    { new Guid("9e5b1956-e4cc-4de8-8316-0fbe83c9c2e5"), null, "James Joyce", false, null },
                    { new Guid("a20b0c8f-e2cc-4b5b-85f2-c3a570796e74"), null, "Elsa Morante", false, null },
                    { new Guid("a3d4426f-67f8-43b1-8901-750bcea04e20"), null, "Saadi", false, null },
                    { new Guid("a714c1c6-1705-478f-8231-b293e111995e"), null, "Homer", false, null },
                    { new Guid("ab273ba4-c96c-4092-81c1-750ee3c71dbb"), null, "Toni Morrison", false, null },
                    { new Guid("ae2bd410-b110-40c4-b56f-31be18fc1923"), null, "Vyasa", false, null },
                    { new Guid("af0828c7-f49e-4d1b-a888-4e06335dcd04"), null, "Lu Xun", false, null },
                    { new Guid("af7982e7-da2d-4bc2-a5aa-968752029299"), null, "Doris Lessing", false, null },
                    { new Guid("b13f8b26-6a6e-4574-a2bc-ae28caf99124"), null, "Salman Rushdie", false, null },
                    { new Guid("b28d9964-eabe-4a33-911e-a861b6b71a70"), null, "Ernest Hemingway", false, null },
                    { new Guid("b8894cea-2e38-4713-8b97-06d7bbf9c8aa"), null, "D. H. Lawrence", false, null },
                    { new Guid("ba51caa3-77fe-43b7-ba2e-d78b13cfafc2"), null, "Ovid", false, null },
                    { new Guid("bc851c3a-6ffa-4b74-bd8e-36ba2b04d4fb"), null, "Jane Austen", false, null },
                    { new Guid("be5885ea-c04f-4c48-91a4-31d9bba8d2c2"), null, "Knut Hamsun", false, null },
                    { new Guid("c264ed2e-b98f-4f00-b055-65a327f1b2e0"), null, "Nikos Kazantzakis", false, null },
                    { new Guid("c3a5174b-5a8a-4076-9a6f-3b2eda4744ba"), null, "Leo Tolstoy", false, null },
                    { new Guid("c6d892db-d741-42a6-a4e4-d23fcc4c4739"), null, "Mark Twain", false, null },
                    { new Guid("ca4895a4-5aad-4355-975a-efbdd1ff1715"), null, "Honoré de Balzac", false, null },
                    { new Guid("ce5f3664-f332-4bcd-a7f9-b413733e94dc"), null, "Halldór Laxness", false, null },
                    { new Guid("d556a20d-6dba-4494-b0ec-ae2e82eacf3b"), null, "Naguib Mahfouz", false, null },
                    { new Guid("da83a1a0-6d2d-431d-9fd0-05d7b13b3f79"), null, "Gabriel García Márquez", false, null },
                    { new Guid("dbe21766-6473-4ed9-a130-17efc9f55310"), null, "Rumi", false, null },
                    { new Guid("ddf5c81b-6351-48c0-863d-adab65acaefc"), null, "Giacomo Leopardi", false, null },
                    { new Guid("de81ae05-a296-450b-aaa9-61d04448930e"), null, "Jorge Luis Borges", false, null },
                    { new Guid("e09e0cf4-f001-49b2-8486-698c970991d3"), null, "Emily Brontë", false, null },
                    { new Guid("eadcdc57-979f-4278-8f3f-0d154a09abed"), null, "Miguel de Cervantes", false, null },
                    { new Guid("ec43876e-7cac-436c-a115-55642490e19b"), null, "Juan Rulfo", false, null },
                    { new Guid("ecad5f48-371a-4a60-8e5e-bbf03347c2a3"), null, "Fernando Pessoa", false, null },
                    { new Guid("ef7c9244-7559-4fc9-a42b-6c1f6050a236"), null, "Federico García Lorca", false, null },
                    { new Guid("f7df9789-4cd7-49d3-af01-ebe6707b138a"), null, "Louis-Ferdinand Céline", false, null },
                    { new Guid("fbec15ce-53b0-442a-97f7-9dd03d6a43c2"), null, "Fyodor Dostoevsky", false, null }
                });
        }
    }
}
