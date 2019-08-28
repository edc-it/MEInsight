# IANA Tz Data

***Unofficial*** JSON distribution of *zdumped* [IANA timezone data][].

It should be noted that a tabular text file (not JSON) is the "official" format for the [IANA timezone data][]. The JSON data contained in this package is provided as a convenience for the development community, and is programatically generated from the corresponding text files using the JSON conversion utility provided here.

## Goals

- The data in this package is intended to serve as a common reference point for most JavaScript packages.
- Allow i18n libraries to define IANA timezone data as versioned dependency.

### What's not included

No code other than the conversion utility is included.

The official data has additional information and has a different structure than what's distributed here. The official data has Zones and Rules

### What's included

A JSON representation of the timezone transitions (actually, the output of `zdump -v`) for every timezone ids, which is what moment-timezone, globalize, and perhaps other JavaScript library use to calculate a date in a specific timezone ids. The structure looks like the below.

```
{
  "zoneData": {
    ...
    "America": {
      ...
      "New_York": {
        abbrs: [],
        untils: [],
        offsets: [],
        isdsts: []
      }
      ...
    }
  }
}
```

## Status

Latest official release is version 2019b, published on 2019-07-01.

## Usage

Installation using [NPM](https://www.npmjs.com):

```
npm install --save iana-tz-data
```

We follow a semver corresponding version based on the official version. The major version corresponds to the year and the minor corresponds to the letter. The patch version is independent and used for any necessary package fixes.

| Official version | Our corresponding semver version |
| ---------------- | -------------------------------- |
| `2019b`          | `2019.1`                         |
| `2018i`          | `2018.8`                         |
| `2018g`          | `2018.6`                         |
| `2018f`          | `2018.5`                         |
| `2018e`          | `2018.4`                         |
| `2018d`          | `2018.3`                         |
| `2018c`          | `2018.2`                         |
| `2018b`          | `2018.1`                         |
| `2017c`          | `2017.2`                         |
| `2017b`          | `2017.1`                         |
| `2017a`          | `2017.0`                         |

On your application, you can access IANA timezone JSON data by importing the `"iana-tz-data"` module.

```javascript
import IANATimezoneData from "iana-tz-data";
```

## License

MIT © [Rafael Xavier de Souza](http://rafael.xavier.blog.br)

[IANA timezone data]: https://www.iana.org/time-zones
