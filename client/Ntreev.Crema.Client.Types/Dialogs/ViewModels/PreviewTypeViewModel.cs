﻿//Released under the MIT License.
//
//Copyright (c) 2018 Ntreev Soft co., Ltd.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//documentation files (the "Software"), to deal in the Software without restriction, including without limitation the 
//rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit 
//persons to whom the Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Ntreev.Crema.Client.Types.Properties;
using Ntreev.Crema.Client.Framework;
using Ntreev.Crema.Services;
using Ntreev.Crema.Data;
using Ntreev.ModernUI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Crema.Client.Types.Dialogs.ViewModels
{
    class PreviewTypeViewModel : ModalDialogBase
    {
        private readonly Authentication authentication;
        private readonly IType type;
        private readonly long revision;
        private CremaDataType source;

        public PreviewTypeViewModel(Authentication authentication, IType type, long revision)
        {
            this.authentication = authentication;
            this.type = type;
            this.revision = revision;
            this.Initialize();
        }

        public CremaDataType Source
        {
            get { return this.source; }
            private set
            {
                this.source = value;
                this.NotifyOfPropertyChange(nameof(this.Source));
            }
        }

        private async void Initialize()
        {
            try
            {
                this.DisplayName = await this.type.Dispatcher.InvokeAsync(() => $"{this.type.Name} - {revision}");
                this.BeginProgress(Resources.Message_ReceivingInfo);
                this.Source = await Task.Run(() =>
                {
                    var dataSet = this.type.GetDataSet(this.authentication, this.revision);
                    return dataSet.Types.FirstOrDefault();
                });
            }
            catch (Exception e)
            {
                AppMessageBox.ShowError(e);
                this.TryClose();
            }
            finally
            {
                this.EndProgress();
            }
        }
    }
}
