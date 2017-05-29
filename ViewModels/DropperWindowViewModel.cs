using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Media;
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
        private CompositeDisposable disposable = new CompositeDisposable();

        /// <summary>
        /// モデル部を取得します。
        /// </summary>
        public Models.DropperWindowsModel Model { get; } = new Models.DropperWindowsModel();

        /// <summary>
        /// R値を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<byte> R { get; }

        /// <summary>
        /// G値を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<byte> G { get; }

        /// <summary>
        /// B値を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<byte> B { get; }

        /// <summary>
        /// マウスカーソルのX座標を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<int> X { get; }

        /// <summary>
        /// マウスカーソルのY座標を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<int> Y { get; }

        /// <summary>
        /// マウスがクリックされているかどうかを表すフラグを管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsClicked { get; }

        /// <summary>
        /// RGB値を表す文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> RGB { get; }

        /// <summary>
        /// マウスカーソル座標を表す文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> Coordinate { get; }

        /// <summary>
        /// 単色ブラシを管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<SolidColorBrush> Brush { get; }

        /// <summary>
        /// クリックされたかどうかを表すフラグを取得します。
        /// </summary>
        public bool Confirmed { get; private set; }

        /// <summary>
        /// ウィンドウを閉じるリクエストを取得します。
        /// </summary>
        public InteractionRequest<Notification> CloseRequest { get; } = new InteractionRequest<Notification>();

        public DropperWindowViewModel()
        {
            //  カラーコード
            R = Model.ColorCode.ObserveProperty(c => c.R).ToReadOnlyReactiveProperty().AddTo(disposable);
            G = Model.ColorCode.ObserveProperty(c => c.G).ToReadOnlyReactiveProperty().AddTo(disposable);
            B = Model.ColorCode.ObserveProperty(c => c.B).ToReadOnlyReactiveProperty().AddTo(disposable);
            var colorChangedAsObservable = Model.ColorCode.PropertyChangedAsObservable().Publish().RefCount();
            RGB = colorChangedAsObservable.Select(_ => $" RGB({R.Value}, {G.Value}, {B.Value})").ToReadOnlyReactiveProperty().AddTo(disposable);
            Brush = colorChangedAsObservable.Select(_ => new SolidColorBrush(Color.FromRgb(R.Value, G.Value, B.Value))).ToReadOnlyReactiveProperty().AddTo(disposable);

            //  マウスカーソル
            X = Model.Cursor.ObserveProperty(c => c.X).ToReadOnlyReactiveProperty().AddTo(disposable);
            Y = Model.Cursor.ObserveProperty(c => c.Y).ToReadOnlyReactiveProperty().AddTo(disposable);
            IsClicked = Model.Cursor.ObserveProperty(c => c.IsClicked).ToReadOnlyReactiveProperty().AddTo(disposable);

            //  イベントの購読
            var coordChangedAsObservable = Observable.Merge(X, Y).Publish().RefCount();
            Coordinate = coordChangedAsObservable.Select(_ => $" 座標({X.Value}, {Y.Value})").ToReadOnlyReactiveProperty().AddTo(disposable);
            coordChangedAsObservable.Subscribe(_ => Model.UpdateColorCode(X.Value, Y.Value)).AddTo(disposable);
            IsClicked.Where(c => c).ObserveOnDispatcher().Subscribe(_ => { Confirmed = true; CloseRequest.Raise(new Notification()); }).AddTo(disposable);

            Model.AddTo(disposable);
            Model.Timer.Start();
        }

        /// <summary>
        /// リソースの破棄を行います。
        /// </summary>
        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}