import { trigger, transition, style, animate } from '@angular/animations';

export const delayedFadeIn = (transitionDuration: number) =>
  trigger('fadeIn', [
    transition(':enter', [
      style({
        opacity: 0,
      }),
      animate(
        transitionDuration,
        style({
          opacity: 1,
        })
      ),
    ]),
  ]);

export const fadeIn = delayedFadeIn(350);
