using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ColorFinder.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace ColorFinder.ViewModels
{
    /// <summary>
    /// スポイトウィンドウに対するViewModelを提供します。
    /// </summary>
    public class DropperWindowViewModel
    {
        //  カラーコード
        private ColorCode colorCode = new ColorCode();

        //  マウスカーソル
        private MouseCursor mouseCursor = new MouseCursor();

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
        /// マウスカーソルのX座標を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<int> X { get; private set; }

        /// <summary>
        /// マウスカーソルのY座標を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<int> Y { get; private set; }

        /// <summary>
        /// マウスがクリックされているかどうかを表すフラグを管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<bool> IsClicked { get; private set; }

        /// <summary>
        /// RGB値を表す文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> RGB { get; private set; }

        /// <summary>
        /// マウスカーソル座標を表す文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> Coordinate { get; private set; }

        /// <summary>
        /// 単色ブラシを管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<SolidColorBrush> Brush { get; private set; }

        //  マウスカーソル更新用のタイマー
        private ReactiveTimer timer = new ReactiveTimer(TimeSpan.FromMilliseconds(50));

        public DropperWindowViewModel()
        {
            //  RGB値を管理するReactivePropertyを生成する
            R = colorCode.ToReactivePropertyAsSynchronized(c => c.R);
            G = colorCode.ToReactivePropertyAsSynchronized(c => c.G);
            B = colorCode.ToReactivePropertyAsSynchronized(c => c.B);

            //  マウスを管理するReactivePropertyを生成する
            X = mouseCursor.ToReactivePropertyAsSynchronized(m => m.X);
            Y = mouseCursor.ToReactivePropertyAsSynchronized(m => m.Y);
            IsClicked = mouseCursor.ToReactivePropertyAsSynchronized(m => m.IsClicked);

            //  RGB値が変更通知を発行したときに更新する読み取り専用プロパティを生成する
            var rgb = Observable.Merge(R, G, B);
            RGB = rgb.Select(_ => $" RGB({R.Value}, {G.Value}, {B.Value})").ToReadOnlyReactiveProperty();
            Brush = rgb.Select(_ => new SolidColorBrush(Color.FromRgb(R.Value, G.Value, B.Value))).ToReadOnlyReactiveProperty();

            //  マウスカーソル座標が変更通知を発行したときに更新する読み取り専用プロパティを生成する
            Coordinate = Observable.Merge(X, Y).Select(_ => $" 座標({X.Value}, {Y.Value})").ToReadOnlyReactiveProperty();
        }
    }
}