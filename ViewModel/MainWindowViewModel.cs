using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Windows.Media;
using ColorFinder.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace ColorFinder.ViewModel
{
    /// <summary>
    /// メインウィンドウに対するViewModelを提供します。
    /// </summary>
    public class MainWindowViewModel
    {
        //  カラーコード
        private ColorCode colorCode = new ColorCode();

        /// <summary>
        /// R値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> R { get; private set; }

        /// <summary>
        /// G値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> G { get; private set; }

        /// <summary>
        /// B値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> B { get; private set; }

        /// <summary>
        /// 10進数表記文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> Decimal { get; private set; }

        /// <summary>
        /// 16進数表記文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> Hexadecimal { get; private set; }

        /// <summary>
        /// 単色ブラシを管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<SolidColorBrush> Brush { get; private set; }

        /// <summary>
        /// 最前面表示を行うかどうかを表すフラグを管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<bool> Topmost { get; private set; } = new ReactiveProperty<bool>();

        /// <summary>
        /// ランダムな色を設定するコマンドを取得します。
        /// </summary>
        public ReactiveCommand RandomCommand { get; private set; } = new ReactiveCommand();

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        public MainWindowViewModel()
        {
            //  RGB値を管理するReactivePropertyを生成する
            R = colorCode.ToReactivePropertyAsSynchronized(c => c.R);
            G = colorCode.ToReactivePropertyAsSynchronized(c => c.G);
            B = colorCode.ToReactivePropertyAsSynchronized(c => c.B);

            //  RGB値が変更通知を発行したときに更新する
            //  読み取り専用プロパティを生成する
            var rgb = Observable.Merge(R, G, B);
            Decimal = rgb.Select(_ => $"{R.Value}, {G.Value}, {B.Value}").ToReadOnlyReactiveProperty();
            Hexadecimal = rgb.Select(_ => $"#{R.Value.ToString("X2")}{G.Value.ToString("X2")}{B.Value.ToString("X2")}").ToReadOnlyReactiveProperty();
            Brush = rgb.Select(_ => new SolidColorBrush(Color.FromRgb(R.Value, G.Value, B.Value))).ToReadOnlyReactiveProperty();
            R.Value = G.Value = B.Value = 255;

            //  コマンドを設定する
            RandomCommand.Subscribe(_ => colorCode.SetRandomly());
        }
    }
}