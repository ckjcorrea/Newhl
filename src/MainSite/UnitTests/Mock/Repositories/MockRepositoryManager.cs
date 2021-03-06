﻿using Moq;
using System.Collections;
using System.Collections.Generic;
using Newhl.MainSite.DataLayer.Repositories;

namespace Newhl.MainSite.UnitTests.Mock.Repositories
{
    public class MockRepositoryManager : IRepositoryManager
    {
        Mock<IAMFUserRepository> mockUserRepository;

        public IAMFUserRepository UserRepository
        {
            get
            {
                if (this.mockUserRepository == null)
                {
                    this.mockUserRepository = new Mock<IAMFUserRepository>();
                    MockUserRepositoryHelper.ConfigureAllMethods(this.mockUserRepository);
                }

                return this.mockUserRepository.Object;
            }
        }

        Mock<ILoginAttemptRepository> mockLoginAttemptRepository;

        public ILoginAttemptRepository LoginAttemptRepository
        {
            get
            {
                if (this.mockLoginAttemptRepository == null)
                {
                    this.mockLoginAttemptRepository = new Mock<ILoginAttemptRepository>();
                    MockLoginAttemptRepository.ConfigureAllMethods(this.mockLoginAttemptRepository);
                }

                return this.mockLoginAttemptRepository.Object;
            }
        }

        Mock<ISeasonRepository> mockProgramRepository;

        public ISeasonRepository SeasonRepository
        {
            get
            {
                if (this.mockProgramRepository == null)
                {
                    this.mockProgramRepository = new Mock<ISeasonRepository>();
                    MockProgramRepository.ConfigureAllMethods(this.mockProgramRepository);
                }

                return this.mockProgramRepository.Object;
            }
        }

        Mock<IPlayerSeasonRepository> mockPlayerSeasonRepository;

        public IPlayerSeasonRepository PlayerSeasonRepository
        {
            get
            {
                if (this.mockPlayerSeasonRepository == null)
                {
                    this.mockPlayerSeasonRepository = new Mock<IPlayerSeasonRepository>();
                    MockPlayerSeasonRepository.ConfigureAllMethods(this.mockPlayerSeasonRepository);
                }

                return this.mockPlayerSeasonRepository.Object;
            }
        }
    }
}
