namespace CAUtility
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    ///  XMLを読み込みシリアライズ/デシリアライズを行います。
    /// </summary>
    public sealed class XmlSetting
    {
        /// <summary>エンコード</summary>
        private static UTF8Encoding utf = new UTF8Encoding(false);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private XmlSetting()
        {
            throw new NotImplementedException("インスタンス化できません。");
        }
        /// <summary>
        /// XMLロード
        /// </summary>
        /// <typeparam name="T">マッピングするクラスタイプ</typeparam>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>マッピングされたインスタンス</returns>
        public static T Load<T>(string filePath)
        {
            using (var streamReader = new StreamReader(filePath, utf))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(streamReader);
            }
        }
        /// <summary>
        /// XMLにセーブ
        /// </summary>
        /// <typeparam name="T">マッピングするクラスタイプ</typeparam>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="instance">XML化するインスタンス</param>
        public static void Save<T>(string filePath, T instance)
        {
            using (var streamWriter = new StreamWriter(filePath, true, utf))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(streamWriter, instance);
            }
        }
    }
}