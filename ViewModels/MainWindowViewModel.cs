using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Media;
using ColorFinder.Models;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace ColorFinder.ViewModels
{
    /// <summary>
    /// メインウィンドウに対するViewModelを提供します。
    /// </summary>
    public class MainWindowViewModel
    {
        private ColorCode colorCode = new ColorCode();

        private CompositeDisposable disposable = new CompositeDisposable();

        /// <summary>
        /// R値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> R { get; }

        /// <summary>
        /// G値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> G { get; }

        /// <summary>
        /// B値を管理するプロパティを取得します。
        /// </summary>
        public ReactiveProperty<byte> B { get; }

        /// <summary>
        /// 10進数表記文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> Decimal { get; }

        /// <summary>
        /// 16進数表記文字列を管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<string> Hexadecimal { get; }

        /// <summary>
        /// 単色ブラシを管理するプロパティを取得します。
        /// </summary>
        public ReadOnlyReactiveProperty<SolidColorBrush> Brush { get; }

        /// <summary>
        /// ランダムな色を設定するコマンドを取得します。
        /// </summary>
        public ReactiveCommand RandomCommand { get; } = new ReactiveCommand();

        /// <summary>
        /// スポイト機能を提供するコマンドを取得します。
        /// </summary>
        public ReactiveCommand DropperCommand { get; } = new ReactiveCommand();

        /// <summary>
        /// スポイト機能を提供するコマンドのリクエストを取得します。
        /// </summary>
        public InteractionRequest<Confirmation> ShowDropperDialogRequest { get; } = new InteractionRequest<Confirmation>();

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        public MainWindowViewModel()
        {
            //  RGB値を管理するReactivePropertyを生成する
            R = colorCode.ToReactivePropertyAsSynchronized(c => c.R).AddTo(disposable);
            G = colorCode.ToReactivePropertyAsSynchronized(c => c.G).AddTo(disposable);
            B = colorCode.ToReactivePropertyAsSynchronized(c => c.B).AddTo(disposable);

            //  RGB値が変更通知を発行したときに更新する
            //  読み取り専用プロパティを生成する
            var rgb = Observable.Merge(R, G, B);
            Decimal = rgb.Select(_ => $"{R.Value}, {G.Value}, {B.Value}").ToReadOnlyReactiveProperty().AddTo(disposable);
            Hexadecimal = rgb.Select(_ => $"#{R.Value.ToString("X2")}{G.Value.ToString("X2")}{B.Value.ToString("X2")}").ToReadOnlyReactiveProperty().AddTo(disposable);
            Brush = rgb.Select(_ => new SolidColorBrush(Color.FromRgb(R.Value, G.Value, B.Value))).ToReadOnlyReactiveProperty().AddTo(disposable);
            R.Value = G.Value = B.Value = 255;

            //  コマンドを設定する
            RandomCommand.Subscribe(_ => colorCode.SetRandomly()).AddTo(disposable);
            DropperCommand.Subscribe(_ => ShowDropperDialogRequest.Raise(new Confirmation(), c =>
            {
                if (c.Confirmed)
                {
                    var color = (ColorCode)c.Content;
                    R.Value = color.R;
                    G.Value = color.G;
                    B.Value = color.B;
                }
            }))
            .AddTo(disposable);
        }

        /// <summary>
        /// リソースの破棄を行います。
        /// </summary>
        ~MainWindowViewModel()
        {
            disposable.Dispose();
        }
    }
}