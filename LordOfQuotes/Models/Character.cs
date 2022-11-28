using System;
using LordOfQuotes.Dtos;

namespace LordOfQuotes.Models
{
    public class Character
    {
        public string Id { get; set; }
        public string Height { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Birth { get; set; }
        public string Spouse { get; set; }
        public string Death { get; set; }
        public string Realm { get; set; }
        public string Hair { get; set; }
        public string Name { get; set; }
        public string WikiUrl { get; set; }

        public Character() { }

        public Character(CharacterDto dto)
        {
            Id = dto._id;
            Height = dto.height;
            Race = dto.race;
            Gender = dto.gender;
            Birth = dto.birth;
            Spouse = dto.spouse;
            Death = dto.death;
            Realm = dto.realm;
            Hair = dto.hair;
            Name = dto.name;
            WikiUrl = dto.wikiUrl;
        }
    }
}
