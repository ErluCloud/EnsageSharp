﻿using System;
using System.Collections.Generic;

using Ensage.Common.Menu;
using Ensage.SDK.Menu;

using SharpDX;

using SkywrathMage.Features;
using SkywrathMage;

namespace SkywrathMagePlus
{
    internal class Config : IDisposable
    {
        public SkywrathMagePlus SkywrathMagePlus { get; }

        private MenuFactory Factory { get; }

        public MenuItem<AbilityToggler> AbilityToggler { get; }

        public MenuItem<AbilityToggler> ItemsToggler { get; }

        public MenuItem<AbilityToggler> LinkenBreakerToggler { get; }

        public MenuItem<PriorityChanger> LinkenBreakerChanger { get; }

        public MenuItem<bool> ComboRadiusItem { get; }

        public MenuItem<bool> WDrawItem { get; }

        public MenuItem<KeyBind> ComboKeyItem { get; }

        public MenuItem<KeyBind> SpamKeyItem { get; }

        public MenuItem<bool> WTargetItem { get; }

        public MenuItem<Slider> WRadiusItem { get; }

        public MenuItem<StringList> TargetItem { get; }

        public ComboItems ComboItems { get; }

        public LinkenBreaker LinkenBreaker { get; }

        public MenuItem<AbilityToggler> AntimageBreakerToggler { get; }

        public MenuItem<PriorityChanger> AntimageBreakerChanger { get; }

        public MenuItem<bool> BladeMailItem { get; }

        public MenuItem<bool> EulBladeMailItem { get; }

        private SpamMode SpamMode { get; }

        private bool Disposed { get; set; }

        public Config(SkywrathMagePlus skywrathMagePlus)
        {
            SkywrathMagePlus = skywrathMagePlus;

            Factory = MenuFactory.CreateWithTexture("SkywrathMagePlus", "npc_dota_hero_skywrath_mage");
            Factory.Target.SetFontColor(Color.Aqua);
            var AbilitiesMenu = Factory.Menu("Abilities");
            AbilityToggler = AbilitiesMenu.Item("Use: ", new AbilityToggler(new Dictionary<string, bool>
            {
                { "skywrath_mage_mystic_flare", true },
                { "skywrath_mage_ancient_seal", true },
                { "skywrath_mage_concussive_shot", true },
                { "skywrath_mage_arcane_bolt", true }
            }));

            var ItemsMenu = Factory.Menu("Items");
            ItemsToggler = ItemsMenu.Item("Use: ", new AbilityToggler(new Dictionary<string, bool>
            {
                { "item_ghost", true },
                { "item_shivas_guard", true },
                { "item_dagon_5", true },
                { "item_veil_of_discord", true },
                { "item_ethereal_blade", true },
                { "item_rod_of_atos", true },
                { "item_bloodthorn", true },
                { "item_orchid", true },
                { "item_sheepstick", true }
            }));

            var LinkenBreakerMenu = Factory.MenuWithTexture("Linken Breaker", "item_sphere");
            LinkenBreakerToggler = LinkenBreakerMenu.Item("Use: ", new AbilityToggler(new Dictionary<string, bool>
            {
                { "skywrath_mage_arcane_bolt", true },
                { "item_sheepstick", true},
                { "item_rod_of_atos", true},
                { "item_bloodthorn", true },
                { "item_orchid", true },
                { "item_cyclone", true },
                { "item_force_staff", true },
                { "item_medallion_of_courage", true },
            }));

            LinkenBreakerChanger = LinkenBreakerMenu.Item("Priority: ", new PriorityChanger(new List<string>
            {
                { "skywrath_mage_arcane_bolt" },
                { "item_sheepstick" },
                { "item_rod_of_atos" },
                { "item_bloodthorn" },
                { "item_orchid" },
                { "item_cyclone" },
                { "item_force_staff" },
                { "item_medallion_of_courage" },
            }));

            var AntimageBreakerMenu = Factory.MenuWithTexture("Antimage Breaker", "antimage_spell_shield");
            AntimageBreakerToggler = AntimageBreakerMenu.Item("Use: ", new AbilityToggler(new Dictionary<string, bool>
            {
                { "skywrath_mage_arcane_bolt", true },
                { "item_rod_of_atos", true},
                { "item_cyclone", true },
                { "item_force_staff", true },
                { "item_medallion_of_courage", true },
            }));

            AntimageBreakerChanger = AntimageBreakerMenu.Item("Priority: ", new PriorityChanger(new List<string>
            {
                { "skywrath_mage_arcane_bolt" },
                { "item_rod_of_atos" },
                { "item_cyclone" },
                { "item_force_staff" },
                { "item_medallion_of_courage" },
            }));

            var BladeMailMenu = Factory.MenuWithTexture("Blade Mail", "item_blade_mail");
            BladeMailItem = BladeMailMenu.Item("Cancel Combo", true);
            BladeMailItem.Item.SetTooltip("Cancel Combo if there is enemy Blade Mail");
            EulBladeMailItem = BladeMailMenu.Item("Use Eul", true);
            EulBladeMailItem.Item.SetTooltip("Use Eul if there is BladeMail with ULT");

            var DrawingMenu = Factory.Menu("Drawing");
            ComboRadiusItem = DrawingMenu.Item("Combo Stable Radius", true);
            ComboRadiusItem.Item.SetTooltip("I suggest making a combo in this radius");
            WDrawItem = DrawingMenu.Item("W Show Target", true);

            ComboKeyItem = Factory.Item("Combo Key", new KeyBind('D'));
            SpamKeyItem = Factory.Item("Q Spam Key", new KeyBind('F'));
            WTargetItem = Factory.Item("Use W Only Target", true);
            WRadiusItem = Factory.Item("Use W in Radius", new Slider(1200, 500, 1600));
            TargetItem = Factory.Item("Target", new StringList("Lock", "Default"));

            ComboItems = new ComboItems(this);

            LinkenBreaker = new LinkenBreaker(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                Factory.Dispose();
                SkywrathMagePlus.Context.Particle.Dispose();
                SpamMode.Dispose();
            }

            Disposed = true;
        }
    }
}
