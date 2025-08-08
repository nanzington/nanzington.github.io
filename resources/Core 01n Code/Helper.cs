using Newtonsoft.Json;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using System;
using System.Collections.Generic;
using System.IO;

namespace SadTutorial {
    public static class Helper { 
        public static bool KeyPressed(Keys key) {
            return GameHost.Instance.Keyboard.IsKeyPressed(key);
        }

        static HashSet<Keys> TriggeredHotkeys = new();
        static HashSet<Keys> SecondaryList = new();
        public static bool HotkeyDown(Keys key) {
            if (!TriggeredHotkeys.Contains(key) && GameHost.Instance.Keyboard.IsKeyPressed(key)) {
                TriggeredHotkeys.Add(key);
                return true;
            }

            return false;
        }

        public static void ClearKeys() {
            SecondaryList.Clear();
            foreach (Keys key in TriggeredHotkeys) {
                if (GameHost.Instance.Keyboard.IsKeyDown(key)) {
                    SecondaryList.Add(key);
                }
            }
            TriggeredHotkeys.Clear();

            foreach (Keys key in SecondaryList) {
                TriggeredHotkeys.Add(key);
            }
        }

        public static bool EitherShift() {
            if (GameHost.Instance.Keyboard.IsKeyDown(Keys.LeftShift) || GameHost.Instance.Keyboard.IsKeyDown(Keys.RightShift))
                return true;
            return false;
        }
        public static bool EitherControl() {
            if (GameHost.Instance.Keyboard.IsKeyDown(Keys.LeftControl) || GameHost.Instance.Keyboard.IsKeyDown(Keys.RightControl))
                return true;
            return false;
        }  

        public static ColoredString GetDarker(this ColoredString instance) {

            for (int i = 0; i < instance.Length; i++) {
                instance[i].Foreground = instance[i].Foreground.GetDarker();
            }

            return instance;
        }

        public static void PrintClickable(this SadConsole.Console instance, int x, int y, string str, Action<string> OnClick, string ID) {
            instance.PrintClickable(x, y, new ColoredString(str), OnClick, ID);
        }

        public static void PrintClickable(this SadConsole.Console instance, int x, int y, ColoredString str, Action<string> OnClick, string ID) {
            Point mousePos = new MouseScreenObjectState(instance, GameHost.Instance.Mouse).CellPosition;

            if (mousePos.X >= x && mousePos.X < x + str.Length && mousePos.Y == y) {
                instance.Print(x, y, str.GetDarker());
            }
            else {
                instance.Print(x, y, str);
            }

            if (GameHost.Instance.Mouse.LeftClicked) {
                if (mousePos.X >= x && mousePos.X < x + str.Length && mousePos.Y == y) {
                    OnClick(ID);
                }
            }
        }

        public static void PrintClickable(this SadConsole.Console instance, int x, int y, string str, Action OnClick) {
            instance.PrintClickable(x, y, new ColoredString(str), OnClick);
        }

        public static void PrintClickable(this SadConsole.Console instance, int x, int y, ColoredString str, Action OnClick) {
            Point mousePos = new MouseScreenObjectState(instance, GameHost.Instance.Mouse).CellPosition;

            if (mousePos.X >= x && mousePos.X < x + str.Length && mousePos.Y == y) {
                instance.Print(x, y, str.GetDarker());
            } else {
                instance.Print(x, y, str);
            }

            if (GameHost.Instance.Mouse.LeftClicked) {
                if (mousePos.X >= x && mousePos.X <= x + str.Length && mousePos.Y == y) {
                    OnClick();
                }
            }
        }


        static MemoryStream stream;
        static StreamReader reader;
        static StreamWriter writer;
        public static byte[] ToByteArray(this object instance, JsonSerializerSettings settings = null) {
            stream = new();
            using (writer = new StreamWriter(stream)) {
                var serializer = JsonSerializer.CreateDefault(settings);
                serializer.Serialize(writer, instance);
                writer.Close();
                stream.Close();
            }

            return stream.ToArray();
        }

        public static T FromByteArray<T>(this byte[] input, JsonSerializerSettings settings = null) {
            stream = new(input);
            T output;

            using (reader = new StreamReader(stream)) {
                using (var jsonReader = new JsonTextReader(reader)) {
                    var serializer = JsonSerializer.CreateDefault(settings);

                    output = serializer.Deserialize<T>(jsonReader);
                    stream.Close();
                    reader.Close();
                }
            }

            return output;
        }
    }
}
