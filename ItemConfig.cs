using System.Collections.Generic;
using Terraria.ModLoader.Config;
using Terraria;
using Terraria.ID;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace ItemControl
{
    class ItemConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // tModLoader will automatically populate a public static field named Instance with the active instance of this ModConfig. (It will unload it too.)
        // This reduces code from "mod.GetConfig<ExampleConfigServer>().DisableExampleWings" to "ExampleConfigServer.Instance.DisableExampleWings". It's just a style choice.
        public static ItemConfig Instance;


        [Label("Banned Items")]
        [Tooltip("All Items in this list will be banned from possesion.")]

        public List<ItemDefinition> BannedItems { get; set; } = new List<ItemDefinition>();


        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            if (ItemControl.instance.herosmod != null)
            {
                //find a better alternative?
                if (ItemControl.instance.herosmod.Call("HasPermission", whoAmI, ItemControl.heropermission) is bool result && result)
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
            return false;
        }

        //public static JsonSerializerSettings SerializerSettings => serializerSettings;

        private ConfigSpagetthi Hero = new ConfigSpagetthi();

        public override void OnChanged()
        {
            /*
            if (Main.netMode == NetmodeID.Server)
            {
                Hero = new ConfigSpagetthi(Instance.BannedItems);
                Directory.CreateDirectory(ModConfigPath);
                string filename = "ItemControl_ItemConfig.json";
                string path = Path.Combine(ModConfigPath, filename);
                string json = JsonConvert.SerializeObject(Hero, SerializerSettings);
                File.WriteAllText(path, json);
            }*/
            //ItemEdit.Karl = mod.GetConfig<ItemConfig>();
            ItemEdit.Karl = ModContent.GetInstance<ItemConfig>();
        }

        /*

            from here on litterally EVERYTHING is copied from tmodloader lul. Pls fix configs not saving serverside.

        */
        /*
        public static string ModConfigPath = Path.Combine(Main.SavePath, "Mod Configs");

        private static readonly IList<JsonConverter> converters = new List<JsonConverter>()
        {
            new Newtonsoft.Json.Converters.VersionConverter(),
			//new ColorJsonConverter(),
        };

        public static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            NullValueHandling = NullValueHandling.Ignore,
            Converters = converters,
            ContractResolver = new ReferenceDefaultsPreservingResolver()
        };

    }
    class ReferenceDefaultsPreservingResolver : DefaultContractResolver
    {
        // This approach largely based on https://stackoverflow.com/a/52684798. 
        public abstract class ValueProviderDecorator : IValueProvider
        {
            readonly IValueProvider baseProvider;

            public ValueProviderDecorator(IValueProvider baseProvider)
            {
                if (baseProvider == null)
                    throw new ArgumentNullException();
                this.baseProvider = baseProvider;
            }

            public virtual object GetValue(object target) { return baseProvider.GetValue(target); }

            public virtual void SetValue(object target, object value) { baseProvider.SetValue(target, value); }
        }
        class NullToDefaultValueProvider : ValueProviderDecorator
        {
            //readonly object defaultValue;
            readonly Func<object> defaultValueGenerator;

            //public NullToDefaultValueProvider(IValueProvider baseProvider, object defaultValue) : base(baseProvider) {
            //	this.defaultValue = defaultValue;
            //}

            public NullToDefaultValueProvider(IValueProvider baseProvider, Func<object> defaultValueGenerator) : base(baseProvider)
            {
                this.defaultValueGenerator = defaultValueGenerator;
            }

            public override void SetValue(object target, object value)
            {
                base.SetValue(target, value ?? defaultValueGenerator.Invoke());
                //base.SetValue(target, value ?? defaultValue);
            }
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            if (type.IsClass)
            {
                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                if (ctor != null)
                {
                    object referenceInstance = ctor.Invoke(null);
                    foreach (JsonProperty prop in props.Where(p => p.Readable))
                    {
                        if (!prop.PropertyType.IsValueType)
                        {
                            var a = type.GetMember(prop.PropertyName);
                            if (prop.Writable)
                            {
                                if (prop.PropertyType.GetConstructor(Type.EmptyTypes) != null)
                                {
                                    // defaultValueCreator will create new instance, then get the value from a field in that object. Prevents deserialized nulls from sharing with other instances.
                                    Func<object> defaultValueCreator = () => prop.ValueProvider.GetValue(ctor.Invoke(null));
                                    prop.ValueProvider = new NullToDefaultValueProvider(prop.ValueProvider, defaultValueCreator);
                                }
                                else if (prop.PropertyType.IsArray)
                                {
                                    Func<object> defaultValueCreator = () => (prop.ValueProvider.GetValue(referenceInstance) as Array).Clone();
                                    prop.ValueProvider = new NullToDefaultValueProvider(prop.ValueProvider, defaultValueCreator);
                                }
                            }
                            if (prop.ShouldSerialize == null)
                                prop.ShouldSerialize = instance =>
                                {
                                    object val = prop.ValueProvider.GetValue(instance);
                                    object refVal = prop.ValueProvider.GetValue(referenceInstance);
                                    return !ConfigManager.ObjectEquals(val, refVal);
                                };
                        }
                    }
                }
            }
            return props;
        }
        */
    }
}
