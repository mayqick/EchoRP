using RAGE.Game;
using RAGE.Ui;
using RAGE;
using Player = RAGE.Elements.Player;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;

namespace ClientSide
{
    public class Main : Events.Script
    {
        private int _cam;//Customization camera

        public static HtmlWindow Browser;
        public Main()
        {
            //Events.Add("showAuthPlayerName",    ShowAuthPlayerName);
            ////Events.Add("showCreatorPage",       ShowCreatorPage);

            //Events.Add("cancelCreator", CancelCreator);
            //Events.Add("closeCreator", CloseCustomization);

            ////Events.Add("setChanged_JSON", UpdateShapeJson);
            //Events.Add("setChanged", SetShape);
            ////Events.Add("setFace", SetFace);
            ////Events.Add("setTrueFace", SetFaceFeature);
            //Events.Add("setClothes", SetClothes);

            //Events.Add("cef_setCharacterIntoCreator", cef_setCharacterIntoCreator);
            ////Events.Add("cef_setGender", SetGender);
            ////Events.Add("cameraPointTo", CustomizationCameraPointTo);

        }
        //private void CancelCreator(object[] args)
        //{
        //    DestroyBrowser();
        //    BrowserShowPage("package://statics/html/chars.html");
        //}//отмена создания нового персонажа, возврат к выбору.
        ////private void SetGender(object[] args)            => Events.CallRemote("setGender", args[0].ToString()); // Установка пола персонажу.
        //private void cef_setCharacterIntoCreator(object[] args) => Events.CallRemote("setCharacterIntoCreator"); // Удаляем одежду игроку.
        ////private void ShowCreatorPage(object[] args)
        ////{
        ////    var pos = new Vector3(-813.5496f, 174.5891f, 76.74077f);
        ////    var p = new Vector3(-811.6723f, 175.2313f, 76.74538f);

        ////    _cam = Cam.CreateCamera(Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), true);

        ////    Cam.SetCamCoord(_cam, pos.X, pos.Y, pos.Z);
        ////    Cam.PointCamAtCoord(_cam, p.X, p.Y, p.Z);
        ////    Cam.SetCamActive(_cam, true);
        ////    Cam.RenderScriptCams(true, false, 0, true, false, 0);
        ////    BrowserShowPage("package://statics/html/creator_test.html");
        ////}
        ////private void SetClothes(object[] args) => Events.CallRemote("setClothes", args); //set cloth data
        //private void ShowAuthPlayerName(object[] args)
        //{
        //    BrowserJsCall(args); // Вывод имени пользователя на странице входа и реги.
        //}
        //private void CloseCustomization(object[] args)
        //{
        //    Cam.DestroyCam(_cam, true);
        //    Cam.RenderScriptCams(false, false, 0, true, false, 0);
        //    DestroyBrowser();

        //    Ui.DisplayRadar(true);
        //    Ui.DisplayHud(true);
        //    Chat.Activate(true);
        //    Chat.Show(true);

        //    Cursor.Visible = false;

        //    Events.CallRemote("closeCustomization", args);
        //}
        ////private void CustomizationCameraPointTo(object[] args)
        ////{
        ////    var id = Convert.ToInt32(args[0]);
        ////    switch (id)
        ////    {
        ////        case 4:
        ////            Cam.SetCamCoord(_cam, -813.5496f, 174.5891f, 76.74077f);
        ////            break;
        ////        case 5:
        ////            Cam.SetCamCoord(_cam, -813.5496f, 174.5891f, 76.74077f);
        ////            break;
        ////        case 6:
        ////            Cam.SetCamCoord(_cam, -812.5499f, 174.5891f, 77.54076f);
        ////            break;
        ////        case 7:
        ////            Cam.SetCamCoord(_cam, -813.5496f, 174.5891f, 76.74077f);
        ////            break;
        ////    }
        ////}
        ////private void SetFaceFeature(object[] args)
        ////{
        ////    var set = JsonConvert.DeserializeObject<float[]>(args[0].ToString());
        ////    for (var i = 0; i != set.Length - 3; i++)
        ////        Player.LocalPlayer.SetFaceFeature(i, set[i]);
        ////}
        ////private void SetFace(object[] args)
        ////{
        ////    var set = JsonConvert.DeserializeObject<int[]>(args[0].ToString());

        ////    for (int i = 0; i != set.Length - 3; i++)
        ////    {
        ////        Player.LocalPlayer.SetHeadOverlay(i, set[i], set[i] > 0 ? 1.0f : 0);
        ////        switch (i)
        ////        {
        ////            case 1:
        ////            case 10:
        ////            case 2:
        ////                Player.LocalPlayer.SetHeadOverlayColor(i, 1, set[set.Length - 2], 0);
        ////                break;
        ////            case 5:
        ////            case 8:
        ////                Player.LocalPlayer.SetHeadOverlayColor(i, 2, set[set.Length - 1], 0);
        ////                break;
        ////        }
        ////    }

        ////    Player.LocalPlayer.SetComponentVariation(2, set[set.Length - 3], 0, 0);
        ////    Player.LocalPlayer.SetHairColor(set[set.Length - 2], 0);
        ////}
        //private void SetShape(object[] args) => Player.LocalPlayer.SetHeadBlendData(int.Parse(args[0].ToString()), int.Parse(args[1].ToString()), 0,
        //   int.Parse(args[2].ToString()), int.Parse(args[3].ToString()), 0, float.Parse(args[4].ToString()), float.Parse(args[5].ToString()), 0, false);
        ////private void UpdateShapeJson(object[] args)
        ////{
        ////    var sets = JsonConvert.DeserializeObject<object[]>(args[0].ToString());

        ////    try
        ////    {
        ////        Player.LocalPlayer.SetHeadBlendData(
        ////            int.Parse(sets[0].ToString()),
        ////            int.Parse(sets[1].ToString()), 0,
        ////            int.Parse(sets[2].ToString()),
        ////            int.Parse(sets[3].ToString()), 0,
        ////            float.Parse(sets[4].ToString(), CultureInfo.InvariantCulture),
        ////            float.Parse(sets[5].ToString(), CultureInfo.InvariantCulture), 0, false);
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        Chat.Output(e.Message);
        ////        Console.WriteLine(e.Message);
        ////        Console.WriteLine(e.StackTrace);
        ////    }
        ////}

        //public void BrowserShowPage(string page)
        //{
        //    Ui.DisplayRadar(false);
        //    Ui.DisplayHud(false);
        //    Chat.Activate(false);
        //    Chat.Show(false);

        //    if (!Cursor.Visible)
        //        Cursor.Visible = true;

        //    Browser?.Destroy();
        //    Browser = new HtmlWindow(page);
        //}

        ///// Удаляем объекты браузера, возвращаем UI
        //public void DestroyBrowser()
        //{
        //    Ui.DisplayRadar(true);
        //    Ui.DisplayHud(true);
        //    Chat.Activate(true);
        //    Chat.Show(true);

        //    if (Cursor.Visible)
        //        Cursor.Visible = false;

        //    Browser?.Destroy();
        //    Browser = null;
        //}

        ///// Вызываем JS внутри браузера
        //public void BrowserJsCall(object[] args)
        //{
        //    if (args != null && args.Length > 0)
        //    {
        //        var funcName = (string)args[0];
        //        var jsCode = string.Empty;
        //        if (args.Length > 1)
        //        {
        //            for (int i = 1; i < args.Length; i++)
        //                jsCode += jsCode.Length > 0 ? $", '{(string)args[i]}'" : $"'{(string)args[i]}'";
        //        }
        //        Browser?.ExecuteJs($"{funcName}({jsCode});");
        //    }
        //}
    }
}
