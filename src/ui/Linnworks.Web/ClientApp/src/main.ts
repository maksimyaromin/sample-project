import { enableProdMode, StaticProvider } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { BASE_URL_TOKEN } from './app/common/constants';
import { environment } from './environments/environment';

export function getBaseUrl(): string {
    return document.getElementsByTagName('base')[0].href;
}

const providers: StaticProvider[] = [
    { provide: BASE_URL_TOKEN, useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
