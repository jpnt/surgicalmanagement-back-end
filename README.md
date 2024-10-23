# MVP OperationRequest and Patient API

- .NET 8
- Onion Architecture (with merged infrastructure and application layer)
- [AutoMapper](https://automapper.org/)
- [Repository Pattern](https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/)
- [SqlServer](./SGBD-SETUP.md)
- [Bogus](https://github.com/bchavez/Bogus)


## TODO

- MedicalHistory Entry
- Decouple MedicalHistory Entry from Patient Entity (Database best practice) 
- Notify Planning Module Service
- Delete operation requests if the operation has not yet been scheduled
- Search operation requests by patient name, operation type, priority, and status.
- Only the requesting doctor can update the operation request
- Logs all updates to the operation request (e.g., changes to priority or deadline).
- As patient: Create/Register user profile to book appointments online
- As patient: Update Patient profile
- Tests
- Documentation

## Use Cases

5.1.16 As a Doctor, I want to request an operation, so that the Patient has access to the
necessary healthcare (#16)

5.1.17 As a Doctor, I want to update an operation requisition, so that the Patient has
access to the necessary healthcare (#8)

5.1.18 As a Doctor, I want to remove an operation requisition, so that the healthcare
activities are provided as necessary. (#17)

5.1.19 As a Doctor, I want to list/search operation requisitions, so that I see the details,
edit, and remove operation requisitions (#18)

5.1.3 As a Patient, I want to register for the healthcare application, so that I can create
a user profile and book appointments online. (#23)

5.1.4 As a Patient, I want to update my user profile, so that I can change my personal
details and preferences. (#5)