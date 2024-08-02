'use client';
import {
  Dispatch,
  SetStateAction,
  useCallback,
  useLayoutEffect,
  useMemo,
  useRef,
  useState,
} from 'react';

const isBrowser = typeof window !== 'undefined';
const noop = () => {};

type parserOptions<T> =
  | {
    raw: true;
  }
  | {
    raw: false;
    serializer: (value: T) => string;
    deserializer: (value: string) => T;
  };

export const useLocalStorage = <T>(
  key: string,
  initialValue: T,
  options?: parserOptions<T>,
): [T, Dispatch<SetStateAction<T>>, (val: T) => void] => {
  const deserializer = useMemo(
    () =>
      options
        ? options.raw
          ? (value: unknown) => value
          : options.deserializer
        : JSON.parse,
    [options],
  );

  if (!isBrowser) {
    return [initialValue, noop, noop];
  }

  if (!key) {
    throw new Error('useLocalStorage key may not be falsy');
  }

  // eslint-disable-next-line react-hooks/rules-of-hooks
  const initializer = useRef((key: string) => {
    try {
      const serializer = options
        ? options.raw
          ? String
          : options.serializer
        : JSON.stringify;

      const localStorageValue = localStorage.getItem(key);
      if (localStorageValue !== null) {
        return deserializer(localStorageValue);
      } else {
        initialValue && localStorage.setItem(key, serializer(initialValue));
        return initialValue;
      }
    } catch {
      // If user is in private mode or has storage restriction
      // localStorage can throw. JSON.parse and JSON.stringify
      // can throw, too.
      return initialValue;
    }
  });

  // eslint-disable-next-line react-hooks/rules-of-hooks
  const [state, setState] = useState<T>(() => initializer.current(key));
  type StateUpdaterFn<T> = (prevState: T) => T;
  // eslint-disable-next-line react-hooks/rules-of-hooks
  useLayoutEffect(() => setState(initializer.current(key)), [key]);

  // eslint-disable-next-line react-hooks/rules-of-hooks
  const set: Dispatch<SetStateAction<T>> = useCallback(
    (valOrFunc) => {
      try {
        const newState = typeof valOrFunc === 'function'
          ? (valOrFunc as StateUpdaterFn<T>)(state)
          : valOrFunc;
        if (typeof newState === 'undefined') return;
        let value: string;

        if (options) {
          if (options.raw) {
            if (typeof newState === 'string') value = newState;
            else value = JSON.stringify(newState);
          } else if (options.serializer) value = options.serializer(newState);
          else value = JSON.stringify(newState);
        } else value = JSON.stringify(newState);

        localStorage.setItem(key, value);
        setState(deserializer(value));
      } catch {
        // If user is in private mode or has storage restriction
        // localStorage can throw. Also JSON.stringify can throw.
      }
    },
    [key, setState, deserializer, options, state],
  );

  // eslint-disable-next-line react-hooks/rules-of-hooks
  const remove = useCallback(
    (val: T) => {
      try {
        localStorage.removeItem(key);
        setState(val);
      } catch {
        // If user is in private mode or has storage restriction
        // localStorage can throw.
      }
    },
    [key, setState],
  );

  return [state, set, remove];
};
