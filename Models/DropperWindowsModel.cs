using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace ColorFinder.Models
{
    /// <summary>
    /// スポイトウィンドウで必要なモデルを提供します。
    /// </summary>
    public class DropperWindowsModel : IDisposable
    {
        private CompositeDisposable disposable = new CompositeDisposable();

        /// <summary>
        /// タイマーを取得します。
        /// </summary>
        public ReactiveTimer Timer { get; } = new ReactiveTimer(TimeSpan.FromMilliseconds(20));

        /// <summary>
        /// カラーコードを取得します。
        /// </summary>
        public ColorCode ColorCode { get; } = new ColorCode();

        /// <summary>
        /// マウスカーソルの状態を取得します。
        /// </summary>
        public Cursor Cursor { get; } = new Cursor();

        /// <summary>
        /// スポイト機能を提供します。
        /// </summary>
        public Dropper Dropper { get; } = new Dropper();

        /// <summary>
        /// 初期設定を行います。
        /// </summary>
        public DropperWindowsModel()
        {
            Timer.AddTo(disposable);
            Timer.ObserveOnDispatcher().Subscribe(_ => UpdateCursor()).AddTo(disposable);
        }

        /// <summary>
        /// マウスカーソルの状態を更新します。
        /// </summary>
        public void UpdateCursor()
        {
            PInvoke.GetCursorPos(out Point p);
            Cursor.X = p.X;
            Cursor.Y = p.Y;
            Cursor.IsClicked = PInvoke.GetKeyState(0x01) < 0;
        }

        /// <summary>
        /// カラーコードを更新します。
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        public void UpdateColorCode(int x, int y)
        {
            var color = Dropper.PickUpColor(x, y);
            ColorCode.R = color.R;
            ColorCode.G = color.G;
            ColorCode.B = color.B;
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