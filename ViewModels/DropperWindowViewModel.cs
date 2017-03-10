using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Media;
using ColorFinder.Models;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;


namespace ColorFinder.ViewModels
{
    /// <summary>
    /// スポイトウィンドウに対するViewModelを提供します。
    /// </summary>
    public class DropperWindowViewModel : IDisposable
    {
        //  マウスカーソル
        private MouseCursor mouseCursor = new MouseCursor();

        //  色抽出処理
        private Picker picker = new Picker();

        /// <summary>
        /// R値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> R { get; private set; } = new ReactiveProperty<byte>();

        /// <summary>
        /// G値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> G { get; private set; } = new ReactiveProperty<byte>();

        /// <summary>
        /// B値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> B { get; private set; } = new ReactiveProperty<byte>();

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

        /// <summary>
        /// クリックされたかどうかを表すフラグを取得します。
        /// </summary>
        public bool Confirmed { get; private set; }

        /// <summary>
        /// ウィンドウを閉じるリクエストを取得します。
        /// </summary>
        public InteractionRequest<Notification> CloseRequest { get; } = new InteractionRequest<Notification>();

        //  マウスカーソル更新用のタイマー
        private ReactiveTimer timer = new ReactiveTimer(TimeSpan.FromMilliseconds(50));

        //  後始末用オブジェクト
        private CompositeDisposable disposer = new CompositeDisposable();

        public DropperWindowViewModel()
        {
            //  マウスを管理するReactivePropertyを生成する
            X = mouseCursor.ToReactivePropertyAsSynchronized(m => m.X).AddTo(disposer);
            Y = mouseCursor.ToReactivePropertyAsSynchronized(m => m.Y).AddTo(disposer);
            IsClicked = mouseCursor.ToReactivePropertyAsSynchronized(m => m.IsClicked).AddTo(disposer);

            Observable.Merge(X, Y).Subscribe(_ =>
            {
                var color = picker.PickUpColor(X.Value, Y.Value);
                R.Value = color.R;
                G.Value = color.G;
                B.Value = color.B;
            }).AddTo(disposer);

            //  RGB値が変更通知を発行したときに更新する読み取り専用プロパティを生成する
            var rgb = Observable.Merge(R, G, B);
            RGB = rgb.Select(_ => $" RGB({R.Value}, {G.Value}, {B.Value})").ToReadOnlyReactiveProperty().AddTo(disposer);
            Brush = rgb.Select(_ => new SolidColorBrush(Color.FromRgb(R.Value, G.Value, B.Value))).ToReadOnlyReactiveProperty().AddTo(disposer);

            //  マウスカーソル座標が変更通知を発行したときに更新する読み取り専用プロパティを生成する
            Coordinate = Observable.Merge(X, Y).Select(_ => $" 座標({X.Value}, {Y.Value})").ToReadOnlyReactiveProperty().AddTo(disposer);

            //  タイマーによるマウス状態の更新を行う
            timer.ObserveOn(SynchronizationContext.Current).Subscribe(_ => mouseCursor.Update()).AddTo(disposer);
            timer.AddTo(disposer);
            timer.Start();

            //  マウスがクリックされたときの処理を登録する
            IsClicked.DistinctUntilChanged().Where(c => c).Subscribe(_ => { Confirmed = true; CloseRequest.Raise(new Notification()); }).AddTo(disposer);
        }

        /// <summary>
        /// リソースの破棄を行います。
        /// </summary>
        public void Dispose()
        {
            timer.Stop();
            disposer.Dispose();
        }
    }
}