using BookPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPlatform.Data.Configuration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            //builder.HasData(Seed());
        }

        protected IEnumerable<Character> Seed()
        {
            // SEED INITIAL DATA FROM JSON FILE
            //IEnumerable<Character> charactersToAdd = Deserializer.GenerateCharacters();

            //SEED ADDITIONAL DATA
            IEnumerable<Character> charactersToAdd = new List<Character>()
            {
                new Character()
                {
                }
            };

            return charactersToAdd;
        }
    }
}
