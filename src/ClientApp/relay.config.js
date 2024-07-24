module.exports = {
  language: 'typescript',
  src: './src/.',
  eagerEsModules: true,
  'schema': './schemas/schema.gql',
  customScalars: {
    URL: 'string',
    DateTime: 'string',
    Instant: 'string',
    Uuid: 'string',
    Short: 'number',
    NonNegativeInt: 'number',
    ClaimType: 'string',
  },
};
